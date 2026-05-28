using Microsoft.Extensions.Options;
using VA.AiApi.Auth;
using VA.AiApi.Contracts;
using VA.AiApi.Options;
using VA.AiApi.Services.Abstractions;

namespace VA.AiApi.Services;

public sealed class AiChatService(
    IOllamaChatClient ollamaChatClient,
    IErpUserContextAccessor userContextAccessor,
    IOptions<OllamaOptions> ollamaOptions,
    ILogger<AiChatService> logger) : IAiChatService
{
    public async Task<AiChatResponse> SendAsync(AiChatRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
        {
            throw new ArgumentException("Message is required.", nameof(request));
        }

        var user = userContextAccessor.GetRequired();
        var model = string.IsNullOrWhiteSpace(request.Model) ? ollamaOptions.Value.ChatModel : request.Model.Trim();

        logger.LogInformation(
            "AI chat request. EmployeeId={EmployeeId}; PartCode={PartCode}; Model={Model}",
            user.EmployeeId,
            user.PartCode,
            model);

        var answer = await ollamaChatClient.ChatAsync(request.Message.Trim(), model, cancellationToken);

        return new AiChatResponse
        {
            SessionId = request.SessionId ?? Guid.NewGuid(),
            Answer = answer,
            Model = model,
            CreatedAt = DateTime.Now,
            UserContext = new AiUserContextDto
            {
                CompanyId = user.CompanyId,
                EmployeeId = user.EmployeeId,
                EmployeeName = user.EmployeeName,
                PartCode = user.PartCode,
                Roles = user.Roles
            },
            Sources = [] // Giai đoạn 2 mới có RAG source.
        };
    }
}
