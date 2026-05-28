using VA.AiApi.Contracts;

namespace VA.AiApi.Services.Abstractions;

public interface IAiChatService
{
    Task<AiChatResponse> SendAsync(AiChatRequest request, CancellationToken cancellationToken);
}
