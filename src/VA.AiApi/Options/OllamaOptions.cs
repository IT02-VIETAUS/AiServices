namespace VA.AiApi.Options;

public sealed class OllamaOptions
{
    public const string SectionName = "Ollama";

    public string BaseUrl { get; set; } = "http://localhost:11434";
    public string ChatModel { get; set; } = "qwen2.5:3b";
    public int RequestTimeoutSeconds { get; set; } = 120;
}
