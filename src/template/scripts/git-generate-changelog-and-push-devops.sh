#!/bin/bash
REPOSITORIES=(
  genocs-library
)

# This script is used to build the changelog for all repositories and push it to the DevOps repository.
# It can also be used to pull the latest changes from all repositories if the -p flag is provided.
# if you dont have auto-changelog installed, you can install it globally using npm:
# npm install -g auto-changelog
# Usage:
# If you don't have DevOps repository just create a new repository on GitHub and clone it locally, then run this script from the root of the DevOps repository.

if [ "$1" = "-p" ]
  then
    echo ${REPOSITORIES[@]} | sed -E -e 's/[[:blank:]]+/\n/g' | xargs -I {} -n 1 -P 0 sh -c 'printf "========================================================\nUpdating repository: {}\n========================================================\n"; git -C {} pull; git -C {} checkout develop; '
  else
    for REPOSITORY in ${REPOSITORIES[*]}
    do
      cd $REPOSITORY
      echo 'build changelog on repository: $REPOSITORY'
      auto-changelog --commit-limit false -o ../DevOps/CHANGELOG-$REPOSITORY.md
      cd ..
    done

    echo 'Add new files, Commit changelogs and Push on repository: DevOps'
    cd DevOps
    git add .
    git commit -a -m "Generated changelog for DevOps"
    git push
    cd ..    
fi