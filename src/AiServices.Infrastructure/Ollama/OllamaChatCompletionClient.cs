using System.Net.Http.Json;
using System.Text.Json;
using AiServices.Application.Abstractions.Persistence.Chat;
using Microsoft.Extensions.Options;

namespace AiServices.Infrastructure.Ollama;

/// <summary>
/// Adapter gọi Ollama thật qua HTTP.
/// Client ERP không bao giờ gọi Ollama trực tiếp; chỉ AiServices.Api gọi qua adapter này.
/// </summary>
public sealed class OllamaChatCompletionClient : IChatCompletionClient
{
    private readonly HttpClient _httpClient;
    private readonly OllamaOptions _options;
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public OllamaChatCompletionClient(HttpClient httpClient, IOptions<OllamaOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;

        _httpClient.BaseAddress = new Uri(_options.BaseUrl.TrimEnd('/') + "/");
        _httpClient.Timeout = TimeSpan.FromSeconds(Math.Max(10, _options.RequestTimeoutSeconds));
    }

    public async Task<ChatCompletionResult> CompleteAsync(ChatCompletionRequest request, CancellationToken cancellationToken)
    {
        var ollamaRequest = new OllamaChatRequest
        {
            Model = request.Model,
            Stream = false,
            Messages = request.Messages
                .Select(x => new OllamaMessage { Role = x.Role, Content = x.Content })
                .ToList()
        };

        using var response = await _httpClient.PostAsJsonAsync("api/chat", ollamaRequest, JsonOptions, cancellationToken);
        var raw = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException(
                $"Ollama chat failed. Status={(int)response.StatusCode}. Body={raw}");
        }

        var data = JsonSerializer.Deserialize<OllamaChatResponse>(raw, JsonOptions);
        var content = data?.Message?.Content;

        if (string.IsNullOrWhiteSpace(content))
            throw new InvalidOperationException("Ollama returned an empty chat response.");

        return new ChatCompletionResult
        {
            Content = content,
            Model = data?.Model ?? request.Model,
            RawProviderResponse = raw
        };
    }

    private sealed class OllamaChatRequest
    {
        public required string Model { get; init; }
        public bool Stream { get; init; }
        public required List<OllamaMessage> Messages { get; init; }
    }

    private sealed class OllamaMessage
    {
        public required string Role { get; init; }
        public required string Content { get; init; }
    }

    private sealed class OllamaChatResponse
    {
        public string? Model { get; init; }
        public OllamaMessage? Message { get; init; }
        public bool Done { get; init; }
    }
}
