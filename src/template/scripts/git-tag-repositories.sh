#!/bin/bash
REPOSITORIES=(
  genocs-library
)

# This script processes command line arguments to extract a tag name.

while [[ $# -gt 0 ]]; do
  case "$1" in
    -tag)
      tag_name="$2"
      shift 2
      ;;
    *)
      echo "Usage: $0 -tag <tag_name>"
      exit 1
      ;;
  esac
done

if [ -z "$tag_name" ]; then
  echo "Error: -tag parameter is required and must not be empty."
  echo "Usage: $0 -tag <tag_name>"
  exit 1
fi

echo "Tag name: $tag_name"

if [ "$1" = "-p" ]
  then
    echo ${REPOSITORIES[@]} | sed -E -e 's/[[:blank:]]+/\n/g' | xargs -I {} -n 1 -P 0 sh -c 'printf "========================================================\nUpdating repository: {}\n========================================================\n"; git -C {} pull; git -C {} checkout develop; '
  else
    for REPOSITORY in ${REPOSITORIES[*]}
    do
      echo ========================================================
      echo Updating repository: $REPOSITORY
      echo ========================================================
      cd $REPOSITORY	  
      git pull
      git checkout main
      echo 'creating tag' $tag_name
      echo 'git tag -a' $tag_name '-m' $tag_name
      git tag $tag_name      
      echo ========================================================
      echo Pushing tag $tag_name to remote repository
      echo ========================================================
      git push origin $tag_name
      # If you want to push all tags, you can use:
      git push --tags
      echo ========================================================
      echo Done with repository: $REPOSITORY
      echo ========================================================
      # Go back to the parent directory
      cd ..
    done
fi