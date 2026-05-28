using System.ComponentModel.DataAnnotations;

namespace VA.AiApi.Contracts;

public sealed class AiChatRequest
{
    public Guid? SessionId { get; init; }

    [Required]
    [MinLength(1)]
    public string Message { get; init; } = string.Empty;

    public string? Model { get; init; }
}
