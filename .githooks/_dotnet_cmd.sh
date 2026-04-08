#!/usr/bin/env sh
set -eu

if command -v dotnet >/dev/null 2>&1; then
  echo "dotnet"
elif command -v dotnet.exe >/dev/null 2>&1; then
  echo "dotnet.exe"
elif [ -x "/c/Program Files/dotnet/dotnet.exe" ]; then
  echo "/c/Program Files/dotnet/dotnet.exe"
else
  echo "dotnet executable not found in PATH." >&2
  exit 1
fi

