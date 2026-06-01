#!/bin/bash

set -e

echo "Building and running Application Docker images with Docker Compose..."

cd ./infrastructure/containers/application || {
  echo "Error: could not change to infrastructure/containers/application. Run this script from the repository root." >&2
  exit 1
}

ENV_FILE="./.env"
if [[ ! -f "$ENV_FILE" ]]; then
  echo "Error: $ENV_FILE not found in $(pwd)." >&2
  echo "Create it from the example: cp .env.example .env" >&2
  exit 1
fi

if ! docker compose -f ./docker-compose.override.yml -f ./docker-compose.yml --env-file ./.env --project-name genocs build; then
  echo "Error: Docker Compose build failed." >&2
  exit 1
fi

if ! docker compose -f ./docker-compose.yml --env-file ./.env --project-name genocs up -d; then
  echo "Error: Docker Compose up failed." >&2
  exit 1
fi

cd ../../../ || {
  echo "Error: could not return to the repository root directory." >&2
  exit 1
}

echo "🐳 Ship it! The Application containers are up—unlike that one colleague who is still \"getting coffee.\""
