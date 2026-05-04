#!/bin/bash
MYGET_ENV=""
case "$TRAVIS_BRANCH" in
  "main")
    MYGET_ENV="-dev"
    ;;
esac

dotnet build -c release