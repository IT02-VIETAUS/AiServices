using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VA.AiApi.Auth;
using VA.AiApi.Contracts;
using VA.AiApi.Services.Abstractions;

namespace VA.AiApi.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = ErpTokenAuthenticationDefaults.SchemeName)]
[Route("api/ai/chat")]
public sealed class AiChatController(IAiChatService aiChatService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<AiChatResponse>> SendAsync(
        [FromBody] AiChatRequest request,
        CancellationToken cancellationToken)
    {
        var response = await aiChatService.SendAsync(request, cancellationToken);
        return Ok(response);
    }
}
