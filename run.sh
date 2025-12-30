#!/usr/bin/env bash
# Loads environment variables from .env file and runs dotnet run --watch

# Check if .env file exists
if [ -f .env ]; then
  echo "Loading environment variables from .env file..."
  # Export variables from .env, ignoring comments and empty lines
  set -a
  source .env
  set +a
else
  echo "Warning: .env file not found. Continuing without it..."
fi

# Run dotnet watch
echo "Starting dotnet run --watch..."
cd {{NAMESPACE}}
dotnet run --watch

