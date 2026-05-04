#!/bin/bash
REPOSITORIES=(genocs-library) 

if [[ "$1" = "-p" ]]
  then
    echo ${REPOSITORIES[@]} | sed -E -e 's/[[:blank:]]+/\n/g' | xargs -I {} -n 1 -P 0 sh -c 'printf "========================================================\nPush and commit repository: {}\n========================================================\n"; git add .; git commit -a -m "Updated Packages";git push;'
  else
    for REPOSITORY in ${REPOSITORIES[*]}
    do
      echo ========================================================
      echo Add new files, Commit changes and Push on repository: $REPOSITORY
      echo ========================================================
      cd $REPOSITORY
      git add .
      git commit -a -m "Automatic commit"
      git push
      cd ..
    done
fi