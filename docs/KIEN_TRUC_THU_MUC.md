# Kiến trúc thư mục AiServices

```text
AiServices/
├── AiServices.sln
├── Directory.Build.props
├── src/
│   ├── AiServices.Domain/
│   ├── AiServices.Application/
│   ├── AiServices.Infrastructure/
│   ├── AiServices.Persistence/
│   ├── AiServices.Api/
│   └── AiServices.Worker/
└── docs/
```

## Luồng chat giai đoạn 1

```text
ERP Client
  -> POST /api/ai/chat
  -> AiServices.Api Auth Handler
  -> ErpHttpTokenValidator
  -> AiChatController
  -> AiChatService
  -> IChatCompletionClient
  -> OllamaChatCompletionClient
  -> Ollama Server
```

## Quy tắc giữ kiến trúc sạch

1. Controller không chứa logic dài.
2. Application không phụ thuộc ASP.NET, PostgreSQL, Ollama cụ thể.
3. Infrastructure chỉ làm adapter gọi hệ thống ngoài.
4. Persistence chỉ lo DB/vector/log.
5. API chỉ lo HTTP/auth/middleware/controller.
6. Worker chỉ lo tác vụ nền.
7. Agent không được trộn vào chat thường.
