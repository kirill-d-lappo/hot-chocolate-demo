#!/usr/bin/env pwsh

$ErrorActionPreference = "Stop"

$compose_file = Join-Path "$PSScriptRoot" "compose.yaml"

& {
  $env:COMPOSE_BAKE = "true"
  & docker compose --progress plain -f "$compose_file" up --force-recreate --build -d --pull always
}
