#!/bin/bash
REPOSITORIES=(genocs-library) 

if [[ "$1" = "-p" ]]
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
      #git checkout main
      #echo 'clear local branches'
      #git remote prune origin --dry-run
      #git remote update origin --p
      echo 'number of branches'
      #echo 'remote:'
      git branch -r | wc -l
      #echo 'all:'
      #git branch --all | wc -l
      cd ..
    done
fi