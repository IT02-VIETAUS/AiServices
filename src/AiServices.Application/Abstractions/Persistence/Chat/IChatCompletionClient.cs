namespace AiServices.Application.Abstractions.Persistence.Chat;

public interface IChatCompletionClient
{
    Task<ChatCompletionResult> CompleteAsync(ChatCompletionRequest request, CancellationToken cancellationToken);
}

public sealed class ChatCompletionRequest
{
    public required string Model { get; init; }
    public required IReadOnlyList<ChatMessage> Messages { get; init; }
}

public sealed class ChatMessage
{
    public required string Role { get; init; }
    public required string Content { get; init; }
}

public sealed class ChatCompletionResult
{
    public required string Content { get; init; }
    public required string Model { get; init; }
    public string? RawProviderResponse { get; init; }
}
