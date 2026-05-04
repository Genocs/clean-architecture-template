# Scripts

This folder contains helper scripts for local development, CI/CD packaging, repository maintenance, and release tagging.

## Prerequisites

- Bash scripts require a Unix-like shell (`bash`) and the required tools in `PATH`.
- PowerShell scripts require Windows PowerShell or PowerShell 7+.
- .NET scripts require the .NET SDK.
- Git automation scripts require authenticated `git` access to the target remotes.

## Script reference

### Build and test

- `dotnet-build-release.sh`
	- Runs: `dotnet build -c release`
	- Note: `TRAVIS_BRANCH` is read but currently does not change behavior.

- `dotnet-test.sh`
	- Runs: `dotnet test`

- `run-demo-webapi.sh`
	- Sets `ASPNETCORE_ENVIRONMENT=local`
	- Runs: `dotnet run --project src/demo/WebApi/Host.csproj`

### Packaging and container publishing

- `dotnet-pack-and-publish-nuget.sh`
	- Builds NuGet packages:
		- `Genocs.Core.nuspec`
		- `Genocs.Persistence.MongoDB.nuspec`
	- Version format: `2.0.$TRAVIS_BUILD_NUMBER`
	- Pushes packages only when `TRAVIS_BRANCH=main`
	- Required environment variables:
		- `TRAVIS_BRANCH`
		- `TRAVIS_BUILD_NUMBER`
		- `NUGET_API_KEY`
		- `NUGET_SOURCE`

- `docker-build-and-push.sh`
	- Builds and pushes Docker images for `genocs.core`
	- Tag behavior:
		- `main` -> `latest` and `$TRAVIS_BUILD_NUMBER`
		- `develop` -> `dev` and `dev-$TRAVIS_BUILD_NUMBER`
	- Required environment variables:
		- `TRAVIS_BRANCH`
		- `TRAVIS_BUILD_NUMBER`
		- `DOCKER_CONTAINER`
		- `DOCKER_USERNAME`
		- `DOCKER_PASSWORD`

### Multi-repository helpers

- `git-clone-repositories.sh [-p]`
	- Clones repositories listed in the script (`genocs-library`).
	- Uses URL pattern: `https://github.com/$GITHUB_ORG/_git/<repo>`.
	- `-p` enables parallel clone mode.

- `git-pull-repositories.sh [-p]`
	- Pulls updates for each repository listed in the script.
	- In parallel mode (`-p`), also checks out `develop`.
	- In sequential mode, prints remote branch count.

- `git-commit-and-push-repositories.sh [-p]`
	- Commits and pushes repository changes.
	- Commit message:
		- Sequential mode: `Automatic commit`
		- Parallel mode: `Updated Packages`

- `git-generate-changelog-and-push-devops.sh [-p]`
	- `-p`: updates repositories (`git pull` + `checkout develop`).
	- Default mode:
		- Runs `auto-changelog` for each repository
		- Writes output to `../DevOps/CHANGELOG-<repo>.md`
		- Commits and pushes changes in `DevOps`
	- Requires `auto-changelog` (`npm install -g auto-changelog`).

### Release tagging

- `git-tag-repositories.sh -tag <tag_name>`
	- Requires `-tag` argument.
	- For each repository:
		- `git pull`
		- `git checkout main`
		- creates tag (`git tag <tag_name>`)
		- pushes tag (`git push origin <tag_name>` and `git push --tags`)

- `git-tag-release-and-generate-changelog.sh -tag <tag_name>`
	- Requires `-tag` argument.
	- Resets and updates `DevOps` and all listed repositories.
	- Tags each repository on `main`.
	- Builds changelog files into `DevOps` using `auto-changelog`.
	- Commits and pushes changelog updates from `DevOps`.

### Repository migration/setup

- `git-migrate-remotes.sh`
	- Purpose: clone repositories, validate remotes, create remote repositories, and push branches/tags to a backup remote.
	- Supports repository list from `repositories.txt` (`-f`).
	- Key options:
		- `-p` clone-only mode
		- `-o <url>` clone base URL
		- `-r <url>` new remote base URL (required unless `-p` only)
		- `-c` create repositories on remote provider before migration
		- `-V` validate remotes with `git ls-remote`
		- `-n` dry-run mode
		- `-C <n>` clone concurrency limit
		- `-h` help
	- During remote setup mode, script:
		- resets local state
		- fetches + prunes
		- checks out and pulls `master` and `develop`
		- configures `backup` remote
		- pushes all branches and tags to `backup`
	- Environment variables for repository creation:
		- Azure DevOps: `AZURE_DEVOPS_PAT`
		- GitHub: `GITHUB_TOKEN`

## repositories.txt

`repositories.txt` is a sample input file for `git-migrate-remotes.sh`.

- One repository name per line
- Lines beginning with `#` are comments
- Blank lines are ignored

## PowerShell helper

- `cleanup-temp-and-docx.ps1`
	- Deletes all files under `C:\Temp`
	- Removes `*.docx` files recursively from current directory
	- Use with caution (destructive cleanup)
