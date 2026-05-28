using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using VA.ErpAiClient.Wpf.Models;

namespace VA.ErpAiClient.Wpf.Services;

public sealed class AiApiClient : IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);
    private bool _disposed;

    public AiApiClient(string baseUrl, string erpAccessToken)
    {
        if (string.IsNullOrWhiteSpace(baseUrl))
            throw new ArgumentException("AI API base URL is required.", nameof(baseUrl));

        if (string.IsNullOrWhiteSpace(erpAccessToken))
            throw new ArgumentException("ERP access token is required.", nameof(erpAccessToken));

        _httpClient = new HttpClient
        {
            BaseAddress = new Uri(baseUrl.TrimEnd('/') + "/"),
            Timeout = TimeSpan.FromSeconds(180)
        };

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", erpAccessToken);
    }

    public async Task<AiChatResponse> SendChatAsync(AiChatRequest request, CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.PostAsJsonAsync("api/ai/chat", request, _jsonOptions, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorText = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new InvalidOperationException($"AI API error: {(int)response.StatusCode} {response.ReasonPhrase}. {errorText}");
        }

        var data = await response.Content.ReadFromJsonAsync<AiChatResponse>(_jsonOptions, cancellationToken);
        return data ?? throw new InvalidOperationException("AI API returned empty response.");
    }

    public void Dispose()
    {
        if (_disposed) return;
        _httpClient.Dispose();
        _disposed = true;
    }
}
