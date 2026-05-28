# Chạy file này tại thư mục gốc của bộ starter.
# PowerShell giải thích nhanh:
# - Set-Location: chuyển thư mục làm việc.
# - dotnet restore: tải/khôi phục package cần thiết.
# - dotnet run: chạy project API.

$ErrorActionPreference = "Stop"

$root = Split-Path -Parent $PSScriptRoot
$apiProject = Join-Path $root "src\VA.AiApi\VA.AiApi.csproj"

Write-Host "Restoring AI API..." -ForegroundColor Cyan
dotnet restore $apiProject

Write-Host "Running AI API at http://localhost:5088 ..." -ForegroundColor Green
dotnet run --project $apiProject
