#!/bin/bash
rm -rf build
mkdir build
curl -L https://github.com/genocs/clean-architecture/archive/master.zip -o build/clean-architecture-master.zip
unzip build/clean-architecture-master.zip -d build 
cp -R source/* build

rm -rf build/clean-architecture-master/.all-contributorsrc
rm -rf build/clean-architecture-master/.git
rm -rf build/clean-architecture-master/.gitignore
rm -rf build/clean-architecture-master/appveyor.yml
rm -rf build/clean-architecture-master/docker-compose.yml
rm -rf build/clean-architecture-master/.vscode
rm -rf build/clean-architecture-master/docs
rm -rf build/clean-architecture-master/LICENSE