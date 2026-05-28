using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VA.AiApi.Contracts;
using VA.AiApi.Options;

namespace VA.AiApi.Controllers;

[ApiController]
[Route("api/ai/health")]
public sealed class AiHealthController(IOptions<OllamaOptions> ollamaOptions) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public ActionResult<AiHealthResponse> Get()
    {
        var cfg = ollamaOptions.Value;

        return Ok(new AiHealthResponse
        {
            ServerTime = DateTime.Now,
            OllamaBaseUrl = cfg.BaseUrl,
            ChatModel = cfg.ChatModel
        });
    }
}
