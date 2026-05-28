namespace VA.AiApi.Services.Abstractions;

public interface IOllamaChatClient
{
    Task<string> ChatAsync(string userMessage, string? model, CancellationToken cancellationToken);
}
