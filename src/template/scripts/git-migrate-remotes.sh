#!/bin/bash

## This script allows changing the remote "origin" URL for a list of repositories
## Enhancements:
##  - Global strict mode + error trapping
##  - Usage help (-h)
##  - Support cloning only mode (-p) combined with optional -o for later use
##  - Support creating empty repositories on remote git account (-c)
##  - Validation of required parameters
##  - Clear logging and final success/failure message

set -euo pipefail

# Define the log function
log() {
  printf '[%s] %s\n' "$(date +%H:%M:%S)" "$*"
}

# Define the error function
err() {
  printf '[%s] ERROR: %s\n' "$(date +%H:%M:%S)" "$*" >&2
}

# Define the on_error function
on_error() {
  local exit_code=$1 line=$2 cmd=$3
  FAILED=true
  err "Command '${cmd}' failed at line ${line} with exit code ${exit_code}."
}

# Define the cleanup function
cleanup() {
  if [ "${FAILED}" = true ]; then
    err "Script completed WITH errors."
    exit 1
  else
    log "Script completed successfully."
  fi
}

trap 'on_error $? $LINENO "${BASH_COMMAND}"' ERR
trap cleanup EXIT

usage() {
  cat <<EOF
Usage: $(basename "$0") [-o DEFAULT_CLONE_BASE] [-r NEW_REMOTE_URL] [-f REPO_FILE] [-p] [-n] [-V] [-C N] [-h]

Options:
  -o  Default clone base URL (without trailing repo name)
  -r  New remote URL base URL (without trailing repo name) (required unless -p only)
  -f  Path to a text file containing repository names (one per line, # for comments)
  -p  Clone only mode (just clone repos, do not change origin URLs)
  -c  Create empty repositories on remote git account before other operations
  -n  Dry run (show what would happen, perform no mutating git commands)
  -V  Validate (ping) remote repositories via git ls-remote before making changes
  -C  Concurrency for clone operations (default: 0 meaning unlimited for xargs)
  -h  Show this help

Examples:
  Clone all repositories (parallel) using default embedded list:
    $(basename "$0") -p

  Clone all repositories from external file with limited concurrency and default clone base URL:
    $(basename "$0") -p -f ./scripts/repositories.txt -C 4

  Clone all repositories from external file with limited concurrency
    $(basename "$0") -p -f ./scripts/repositories.txt -C 4 -o https://github.com/Genocs/genocs-library.git

  Create empty repositories on remote git account:
    $(basename "$0") -r https://github.com/Genocs/genocs-library-v2.git -c

  Create empty repositories and then update remote for all repositories:
    $(basename "$0") -o https://github.com/Genocs/genocs-library.git -r https://github.com/Genocs/genocs-library-v2.git -f ./scripts/repositories.txt -c 

  Update remote for all repositories from external file (validate first):
    $(basename "$0") -o https://github.com/Genocs/genocs-library.git -r https://github.com/Genocs/genocs-library-v2.git -f ./scripts/repositories.txt -V 

  Dry run (see actions only):
    $(basename "$0") -o https://github.com/Genocs/genocs-library.git -r https://github.com/Genocs/genocs-library-v2.git -f ./scripts/repositories.txt -n 
EOF
}

# Define the load_repositories function
load_repositories() {
  # Load repository names from REPO_FILE into REPOSITORIES array
  if [ -z "${REPO_FILE}" ]; then
    # Fallback to embedded list
    REPOSITORIES=(
      genocs-library
    )
    return 0
  fi

  if [ ! -f "${REPO_FILE}" ]; then
    err "Repository file not found: ${REPO_FILE}"
    exit 1
  fi

  # shellcheck disable=SC2207
  if command -v mapfile >/dev/null 2>&1; then
    mapfile -t REPOSITORIES < <(grep -Ev '^(#|\s*$)' "${REPO_FILE}" | tr -d '\r') || true
  else
    REPOSITORIES=()
    while IFS= read -r line; do
      line="${line%%$'\r'}" # strip CR
      if [ -n "$line" ] && [ "${line#'#'}" = "$line" ]; then
        REPOSITORIES+=("$line")
      fi
    done < "${REPO_FILE}"
  fi

  if [ ${#REPOSITORIES[@]} -eq 0 ]; then
    err "No repositories loaded from ${REPO_FILE}"
    exit 1
  fi
}

# Wrapper for executing (possibly mutating) commands respecting dry-run mode
run_cmd() {
  if [ "$DRY_RUN" = true ]; then
    printf '[DRY-RUN] %s\n' "$*"
  else
    eval "$@"
  fi
}

# Validate remote repositories exist by attempting ls-remote
validate_remotes() {
  local missing=()
  log "Validating remote accessibility (git ls-remote) ..."
  for r in "${REPOSITORIES[@]}"; do
    local full_url="${NEW_REMOTE_URL%%/}/$r"
    if ! git ls-remote "$full_url" >/dev/null 2>&1; then
      missing+=("$r")
      err "Remote not reachable / missing: $full_url"
    else
      log "OK: $r"
    fi
  done
  if [ ${#missing[@]} -gt 0 ]; then
    err "Validation failed for ${#missing[@]} repository(ies)." 
    if [ "$DRY_RUN" = false ]; then
      exit 2
    fi
  else
    log "All repositories validated successfully."
  fi
}

# Create empty repositories on the remote git account
create_repositories() {
  log "Creating empty repositories on remote git account..."

  # Note: This requires a Personal Access Token (PAT) with appropriate permissions
  # The user should set AZURE_DEVOPS_PAT environment variable
  if [ -z "${AZURE_DEVOPS_PAT:-}" ]; then
    err "AZURE_DEVOPS_PAT environment variable not set. Cannot create Azure DevOps repositories."
    err "Please set AZURE_DEVOPS_PAT with a Personal Access Token that has repository creation permissions."
    exit 1
  fi

  local pat_base64=$(echo -n ":$AZURE_DEVOPS_PAT" | base64 | tr -d '\r\n') 
  local devops_auth="Authorization: Basic ${pat_base64}"
  
  # Extract organization and project from the URL
  # Expected format: https://dev.azure.com/{org}/{project}/_git or https://dev.azure.com/{org}/{project}
  # or https://github.com/{org}
  local org project
  if [[ "$NEW_REMOTE_URL" =~ https://dev.azure.com/([^/]+)/([^/]+)(/_git)?$ ]]; then
    org="${BASH_REMATCH[1]}"
    project="${BASH_REMATCH[2]}"
    log "Detected Azure DevOps: org=$org, project=$project"
    
    # Test PAT permissions by making a simple API call
    log "Testing PAT permissions..."
    local test_url="https://dev.azure.com/${org}/_apis/projects?api-version=7.0"
    if ! curl -s -f "$test_url" \
       -H "${devops_auth}" \
       -H "Accept: application/json" >/dev/null 2>&1; then
      err "PAT authentication failed. Please check your AZURE_DEVOPS_PAT and permissions."
      err "Test URL: $test_url"
      exit 1
    fi
    log "PAT authentication successful"
    
  elif [[ "$NEW_REMOTE_URL" =~ https://github.com/([^/]+) ]]; then
    org="${BASH_REMATCH[1]}"
    project=""
    log "Detected GitHub: org=$org"
  else
    err "Unsupported git provider URL format: $NEW_REMOTE_URL"
    err "Supported formats: https://dev.azure.com/org/project/_git, https://dev.azure.com/org/project, or https://github.com/org"
    exit 1
  fi
  
  # First, get the project ID (GUID) from the project name
  local project_info_url="https://dev.azure.com/${org}/_apis/projects/${project}?api-version=7.0"
  local project_id=""
  
  log "Retrieving project information from: $project_info_url"
  if project_info=$(curl -s -f "$project_info_url" \
     -H "${devops_auth}" \
     -H "Accept: application/json"); then
  echo $project_info
    project_id=$(echo "$project_info" | grep -o '"id":"[^"]*"' | head -1 | cut -d'"' -f4)
  echo $project_id
    
  if [ -z "$project_id" ]; then
    err "Failed to extract project ID from response"
    err "Response: $project_info"
    exit 1
    fi  
    log "Retrieved project ID: ${project_id}"
  else
	local project_error=$(curl -s "$project_info_url" \
     -H "${devops_auth}" \
     -H "Accept: application/json" 2>&1)
  err "Failed to retrieve project information for project: $project"
  err "Error: $project_error"
  exit 1
  fi

  for r in "${REPOSITORIES[@]}"; do
    log "Creating repository: $r"

    if [ "$DRY_RUN" = true ]; then
      log "[DRY-RUN] Would create repository: $r"
      continue
    fi
    
    if [[ "$NEW_REMOTE_URL" =~ https://dev.azure.com ]]; then
      # Azure DevOps API call to create repository
      local api_url="https://dev.azure.com/${org}/${project}/_apis/git/repositories?api-version=7.0"
      local payload="{\"name\":\"${r}\",\"project\":{\"id\":\"${project_id}\"}}"      
	  
      if curl -s -f -X POST "$api_url" \
         -H "${devops_auth}" \
         -H "Content-Type: application/json" \
         -d "$payload" >/dev/null 2>&1; then
        log "Successfully created Azure DevOps repository: $r"
      else
        local error_response=$(curl -s -X POST "$api_url" \
           -H "${devops_auth}" \
           -H "Content-Type: application/json" \
           -d "$payload" 2>&1)
        err "Failed to create Azure DevOps repository: $r"
        err "Error response: $error_response"
        FAILED=true
      fi
      
    elif [[ "$NEW_REMOTE_URL" =~ https://github.com ]]; then
      # GitHub API call to create repository
      local api_url="https://api.github.com/orgs/${org}/repos"
      local payload="{\"name\":\"${r}\",\"private\":true,\"auto_init\":true}"
      
      # Note: This requires a GitHub Personal Access Token (PAT) with appropriate permissions
      # The user should set GITHUB_TOKEN environment variable
      if [ -z "${GITHUB_TOKEN:-}" ]; then
        err "GITHUB_TOKEN environment variable not set. Cannot create GitHub repositories."
        err "Please set GITHUB_TOKEN with a Personal Access Token that has repository creation permissions."
        exit 1
      fi
      
      if curl -s -f -X POST "$api_url" \
         -H "Authorization: token $GITHUB_TOKEN" \
         -H "Accept: application/vnd.github.v3+json" \
         -d "$payload" >/dev/null 2>&1; then
        log "Successfully created GitHub repository: $r"
      else
        err "Failed to create GitHub repository: $r"
        FAILED=true
      fi
    fi
  done
  
  if [ "$FAILED" = true ]; then
    err "Some repositories failed to be created. Check the errors above."
    exit 1
  fi
  
  log "All repositories created successfully."
  
  # Wait a moment for repositories to be fully available
  log "Waiting for repositories to be fully available..."
  sleep 5
}


FAILED=false
CLONE_ONLY=false
DRY_RUN=false
VALIDATE=false
CREATE_REPOS=false
CLONE_CONCURRENCY=0
DEFAULT_CLONE_BASE="https://github.com/Genocs/genocs-library.git"
REPO_FILE=""
NEW_REMOTE_URL=""

while getopts ":o:r:f:C:pncVh" opt; do
  case $opt in
    o) DEFAULT_CLONE_BASE="$OPTARG" ;;
    r) NEW_REMOTE_URL="$OPTARG" ;;
    f) REPO_FILE="$OPTARG" ;;
    C) CLONE_CONCURRENCY="$OPTARG" ;;
    p) CLONE_ONLY=true ;;
    c) CREATE_REPOS=true ;;
    n) DRY_RUN=true ;;
    V) VALIDATE=true ;;
    h) usage; exit 0 ;;
    :) err "Option -$OPTARG requires an argument."; usage; exit 1 ;;
    \?) err "Invalid option: -$OPTARG"; usage; exit 1 ;;
  esac
done

shift $((OPTIND-1))

if [ "$CLONE_ONLY" = false ] && [ -z "$NEW_REMOTE_URL" ]; then
  err "-r NEW_REMOTE_URL is required when not using -p (clone only)."
  usage
  exit 1
fi

if [ "$CREATE_REPOS" = true ] && [ -z "$NEW_REMOTE_URL" ]; then
  err "-r NEW_REMOTE_URL is required when using -c (create repositories)."
  usage
  exit 1
fi

load_repositories


if [ "$VALIDATE" = true ] && [ "$CLONE_ONLY" = false ]; then
  if [ -z "$NEW_REMOTE_URL" ]; then
    err "Validation (-V) requires -r NEW_REMOTE_URL."
    exit 1
  fi
  validate_remotes
fi

# Create repositories if requested
if [ "$CREATE_REPOS" = true ]; then
  log "Creating repositories as requested"
  create_repositories
fi

if [ "$CLONE_ONLY" = true ]; then
  log "Cloning repositories in parallel as requested... (concurrency=${CLONE_CONCURRENCY})"
  # Strip trailing slash
  CLONE_BASE="${DEFAULT_CLONE_BASE%%/}"
  if [ "$DRY_RUN" = true ]; then
    for r in "${REPOSITORIES[@]}"; do
      log "[DRY-RUN] git clone \"${CLONE_BASE}/${r}\""
    done
  else
    # Use a more robust approach that properly handles variable expansion
    for r in "${REPOSITORIES[@]}"; do
      (
        printf "========================================================\n"
        printf "Cloning repository: %s\n" "$r"
        printf "========================================================\n"
        git clone "${CLONE_BASE}/${r}"
      ) &
      
      # Limit concurrency if specified
      if [ "$CLONE_CONCURRENCY" -gt 0 ]; then
        # Wait for background jobs to complete if we've reached the limit
        while [ $(jobs -r | wc -l) -ge "$CLONE_CONCURRENCY" ]; do
          sleep 0.1
        done
      fi
    done
    
    # Wait for all background jobs to complete
    wait
  fi
  exit 0
fi

for REPOSITORY in "${REPOSITORIES[@]}"; do
  log "Processing repository as requested: ${REPOSITORY}"
  if [ ! -d "$REPOSITORY" ]; then
    err "Directory $REPOSITORY does not exist. Skipping."
    continue
  fi

  pushd "$REPOSITORY" >/dev/null
  log "Reset local changes"
  run_cmd git reset --hard

  log "Fetch"
  run_cmd git fetch --all --prune

  log "Checkout master branch"
  run_cmd git checkout master

  log "Pull"
  run_cmd git pull  

  log "Checkout develop branch"
  run_cmd git checkout develop

  log "Pull"
  run_cmd git pull

  # Add here other branches to checkout and pull

  #if git remote | grep -q '^origin$'; then
  #  log 'Remove current origin'
  #  run_cmd git remote remove origin
  #fi

  log "Add new remote"
  run_cmd git remote add backup "$NEW_REMOTE_URL/${REPOSITORY}" || git remote set-url backup "$NEW_REMOTE_URL/${REPOSITORY}"

  log "Show remotes"
  git remote -v

  log 'Push all branches to the new remote'
  run_cmd git push backup --all

  log "Push tags to the new remote"
  run_cmd git push backup --tags

  popd >/dev/null
done
