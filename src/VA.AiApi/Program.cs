using VA.AiApi.Auth;
using VA.AiApi.Infrastructure.Auth;
using VA.AiApi.Infrastructure.Ollama;
using VA.AiApi.Options;
using VA.AiApi.Services;
using VA.AiApi.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<OllamaOptions>(builder.Configuration.GetSection(OllamaOptions.SectionName));
builder.Services.Configure<ErpAuthOptions>(builder.Configuration.GetSection(ErpAuthOptions.SectionName));

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

builder.Services.AddAuthentication(ErpTokenAuthenticationDefaults.SchemeName)
    .AddScheme<ErpTokenAuthenticationOptions, ErpTokenAuthenticationHandler>(
        ErpTokenAuthenticationDefaults.SchemeName,
        options => { });

builder.Services.AddAuthorization();

builder.Services.AddSingleton<IErpTokenValidator, DemoErpTokenValidator>();
builder.Services.AddScoped<IErpUserContextAccessor, HttpErpUserContextAccessor>();
builder.Services.AddScoped<IAiChatService, AiChatService>();

builder.Services.AddHttpClient<IOllamaChatClient, OllamaChatClient>((sp, client) =>
{
    var options = sp.GetRequiredService<Microsoft.Extensions.Options.IOptions<OllamaOptions>>().Value;
    client.BaseAddress = new Uri(options.BaseUrl.TrimEnd('/') + "/");
    client.Timeout = TimeSpan.FromSeconds(options.RequestTimeoutSeconds <= 0 ? 120 : options.RequestTimeoutSeconds);
});

var app = builder.Build();

app.MapGet("/", () => Results.Ok(new
{
    service = "VA AI API",
    phase = "Phase 1 - API foundation",
    health = "/api/ai/health",
    chat = "/api/ai/chat"
})).AllowAnonymous();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
