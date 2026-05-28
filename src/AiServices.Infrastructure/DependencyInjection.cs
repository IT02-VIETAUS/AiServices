
using AiServices.Application.Abstractions.Persistence.Chat;
using AiServices.Application.Abstractions.Services;
using AiServices.Infrastructure.Authentication;
using AiServices.Infrastructure.Ollama;
using AiServices.Infrastructure.Persistence;
using AiServices.Infrastructure.Services.ExternalIds;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AiServices.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddPersistence(configuration)
            .AddAuthenticationServices();

        services.AddScoped<IExternalIdService, ExternalIdServicePostgres>();
        services.Configure<OllamaOptions>(configuration.GetSection(OllamaOptions.SectionName));

        return services;
    }
}
