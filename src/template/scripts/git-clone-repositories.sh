#!/bin/bash
REPOSITORIES=(genocs-library)
GITHUB_ORG=Genocs

if [[ "$1" = "-p" ]]
  then
    echo ${REPOSITORIES[@]} | sed -E -e 's/[[:blank:]]+/\n/g' | xargs -I {} -n 1 -P 0 sh -c 'printf "========================================================\nCloning repository: {}\n========================================================\n"; git clone https://github.com/$GITHUB_ORG/_git/{}'
  else
    for REPOSITORY in ${REPOSITORIES[*]}
    do
      echo ========================================================
      echo Cloning repository: $REPOSITORY
      echo ========================================================
      REPO_URL=https://github.com/$GITHUB_ORG/_git/$REPOSITORY
      git clone $REPO_URL
    done
fi