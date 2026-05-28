namespace AiServices.Application.Abstractions.Persistence.Agent;

/// <summary>
/// Giai đoạn 3-4 sẽ triển khai AI Agent.
/// Interface đặt trước để kiến trúc không phải tách nhánh sau này.
/// </summary>
public interface IAgentOrchestrator
{
    Task<AgentRunResult> RunAsync(AgentRunRequest request, CancellationToken cancellationToken);
}

public sealed class AgentRunRequest
{
    public required string Goal { get; init; }
    public string? SessionId { get; init; }
}

public sealed class AgentRunResult
{
    public required string RunId { get; init; }
    public required string Status { get; init; }
    public required string Summary { get; init; }
}
