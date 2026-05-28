using System;
using System.Collections.Generic;

namespace VA.ErpAiClient.Wpf.Models;

public sealed class AiChatRequest
{
    public Guid? SessionId { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? Model { get; set; }
}

public sealed class AiChatResponse
{
    public Guid SessionId { get; set; }
    public string Answer { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public AiUserContextDto UserContext { get; set; } = new();
    public List<AiSourceDto> Sources { get; set; } = [];
}

public sealed class AiUserContextDto
{
    public Guid CompanyId { get; set; }
    public Guid EmployeeId { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string PartCode { get; set; } = string.Empty;
    public List<string> Roles { get; set; } = [];
}

public sealed class AiSourceDto
{
    public string SourceType { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public double Score { get; set; }
}
