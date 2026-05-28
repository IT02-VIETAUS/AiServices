# Test nhanh AI API.
# Điều kiện:
# 1. Ollama đang chạy.
# 2. Model trong appsettings.json đã có trong Ollama.
# 3. AI API đang chạy tại http://localhost:5088.

$ErrorActionPreference = "Stop"

$baseUrl = "http://localhost:5088"
$token = "dev-erp-token"

Write-Host "1) Test health endpoint" -ForegroundColor Cyan
Invoke-RestMethod -Method Get -Uri "$baseUrl/api/ai/health" | Format-List

Write-Host "2) Test chat endpoint" -ForegroundColor Cyan
$headers = @{
    Authorization = "Bearer $token"
}

$body = @{
    message = "Xin chào, hãy giới thiệu ngắn gọn vai trò của AI nội bộ trong ERP."
} | ConvertTo-Json -Depth 10

$response = Invoke-RestMethod `
    -Method Post `
    -Uri "$baseUrl/api/ai/chat" `
    -Headers $headers `
    -ContentType "application/json" `
    -Body $body

$response | Format-List
