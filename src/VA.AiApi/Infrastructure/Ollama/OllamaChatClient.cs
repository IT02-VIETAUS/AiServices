using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using VA.AiApi.Options;
using VA.AiApi.Services.Abstractions;

namespace VA.AiApi.Infrastructure.Ollama;

public sealed class OllamaChatClient(
    HttpClient httpClient,
    IOptions<OllamaOptions> options,
    ILogger<OllamaChatClient> logger) : IOllamaChatClient
{
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);

    public async Task<string> ChatAsync(string userMessage, string? model, CancellationToken cancellationToken)
    {
        var cfg = options.Value;
        var finalModel = string.IsNullOrWhiteSpace(model) ? cfg.ChatModel : model.Trim();

        var payload = new OllamaChatRequest
        {
            Model = finalModel,
            Stream = false,
            Messages =
            [
                new OllamaMessage
                {
                    Role = "system",
                    Content = "Bạn là AI nội bộ của hệ thống ERP. Ở giai đoạn 1, bạn chỉ trả lời chat demo qua Ollama, chưa được truy xuất tài liệu RAG hoặc dữ liệu nghiệp vụ. Nếu người dùng hỏi dữ liệu nội bộ cụ thể, hãy nói rõ là cần bật RAG/Agent ở giai đoạn sau."
                },
                new OllamaMessage
                {
                    Role = "user",
                    Content = userMessage
                }
            ]
        };

        using var response = await httpClient.PostAsJsonAsync("api/chat", payload, _jsonOptions, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorText = await response.Content.ReadAsStringAsync(cancellationToken);
            logger.LogWarning("Ollama chat failed. Status={StatusCode}; Body={Body}", response.StatusCode, errorText);
            throw new InvalidOperationException($"Ollama chat failed: {(int)response.StatusCode} {response.ReasonPhrase}");
        }

        var result = await response.Content.ReadFromJsonAsync<OllamaChatResponse>(_jsonOptions, cancellationToken);
        return result?.Message?.Content?.Trim() ?? string.Empty;
    }

    private sealed class OllamaChatRequest
    {
        public string Model { get; init; } = string.Empty;
        public bool Stream { get; init; }
        public List<OllamaMessage> Messages { get; init; } = [];
    }

    private sealed class OllamaChatResponse
    {
        public string Model { get; init; } = string.Empty;
        public OllamaMessage? Message { get; init; }
        public bool Done { get; init; }
    }

    private sealed class OllamaMessage
    {
        public string Role { get; init; } = string.Empty;
        public string Content { get; init; } = string.Empty;
    }
}
