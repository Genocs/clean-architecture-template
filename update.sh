#!/bin/bash
rm -rf build
mkdir build
curl -L https://github.com/genocs/clean-architecture-template/archive/master.zip -o build/clean-architecture-master.zip
unzip build/clean-architecture-master.zip -d build 
cp -R src/* build

rm -rf build/template/.all-contributorsrc
rm -rf build/template/.git
rm -rf build/template/.gitignore
rm -rf build/template/appveyor.yml
rm -rf build/template/docker-compose.yml
rm -rf build/template/.vscode
rm -rf build/template/docs
rm -rf build/template/LICENSE