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
    echo '========================================================'
    echo 'Cleanup (Reset hard) repository: DevOps                 '
    echo '========================================================'
    cd DevOps
    echo 'Reset local changes'
    git reset --hard
    echo 'run fetch'
    git fetch
    echo 'fetch completed! run pull'
    git pull
    echo 'pull completed! checkout main branch'
    # Go back to the parent directory
    cd ..

    for REPOSITORY in ${REPOSITORIES[*]}
    do

      echo '========================================================'
      echo 'Cleanup (Reset hard) repository:' $REPOSITORY
      echo '========================================================'
      cd $REPOSITORY
      echo 'Reset local changes'
      git reset --hard
      echo 'run fetch'
      git fetch
      echo 'fetch completed! run pull'
      git pull
      echo 'pull completed! checkout main branch'
      git checkout main
      echo 'number of branches'
      git branch -r | wc -l

      echo '========================================================'
      echo 'Tagging'
      echo '========================================================'
      echo 'creating tag' $tag_name
      echo 'git tag -a' $tag_name '-m' $tag_name
      git tag $tag_name      
      echo '========================================================'
      echo 'Pushing tag' $tag_name 'to remote repository'
      echo '========================================================'
      # To push the tag to the remote repository, you can use:
      git push origin $tag_name
      # If you want to push all tags, you can use:
      git push --tags

      echo '========================================================'
      echo 'Build changelog                                         '
      echo '========================================================'
      auto-changelog --commit-limit false -o ../DevOps/CHANGELOG-$REPOSITORY.md

      echo '========================================================'
      echo 'Done with repository:' $REPOSITORY
      echo '========================================================'
      # Go back to the parent directory
      cd ..
    done

    echo '========================================================'
    echo 'Commit Changelogs and Push on repository: DevOps    '
    echo '========================================================'

    echo 'Add new files, Commit changelogs and Push'
    cd DevOps
    git add .
    git commit -a -m "Generated changelog for tag: $tag_name on repository: DevOps"
    git push
    cd ..    
    echo '========================================================'
    echo ' Finished tagging and changelog generation for all repositories'
    echo '========================================================'
fi