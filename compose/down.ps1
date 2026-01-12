#!/usr/bin/env pwsh

$ErrorActionPreference = "Stop"

$ComposeFile = Join-Path "$PSScriptRoot" "compose.yaml"

& {
  $env:COMPOSE_BAKE = "true"
  & docker compose -f $composeFile down --remove-orphans
}
