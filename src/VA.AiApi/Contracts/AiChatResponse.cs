namespace VA.AiApi.Contracts;

public sealed class AiChatResponse
{
    public Guid SessionId { get; init; }
    public string Answer { get; init; } = string.Empty;
    public string Model { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public AiUserContextDto UserContext { get; init; } = new();
    public List<AiSourceDto> Sources { get; init; } = [];
}

public sealed class AiUserContextDto
{
    public Guid CompanyId { get; init; }
    public Guid EmployeeId { get; init; }
    public string EmployeeName { get; init; } = string.Empty;
    public string PartCode { get; init; } = string.Empty;
    public IReadOnlyList<string> Roles { get; init; } = [];
}

public sealed class AiSourceDto
{
    public string SourceType { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string Path { get; init; } = string.Empty;
    public double Score { get; init; }
}
