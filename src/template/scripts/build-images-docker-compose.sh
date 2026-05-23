#!/bin/bash

cd ./infrastructure/containers/application
# Build with docker compose
docker compose -f ./docker-compose.override.yml -f ./docker-compose.yml --env-file ./.env --project-name genocs build

# Run with docker compose
docker compose -f ./docker-compose.yml --env-file ./.env --project-name genocs up -d
