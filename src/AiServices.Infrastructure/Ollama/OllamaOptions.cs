namespace AiServices.Infrastructure.Ollama;

public sealed class OllamaOptions
{
    public const string SectionName = "Ollama";

    public string BaseUrl { get; init; } = "http://localhost:11434";
    public int RequestTimeoutSeconds { get; init; } = 120;
}
