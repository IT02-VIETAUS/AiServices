namespace VA.AiApi.Contracts;

public sealed class AiHealthResponse
{
    public string Service { get; init; } = "VA AI API";
    public string Phase { get; init; } = "Phase 1 - API foundation";
    public string Status { get; init; } = "OK";
    public DateTime ServerTime { get; init; }
    public string OllamaBaseUrl { get; init; } = string.Empty;
    public string ChatModel { get; init; } = string.Empty;
}
