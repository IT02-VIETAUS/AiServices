
using AiServices.Infrastructure.DatabaseContext.ApplicationDbs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AiServices.Infrastructure.Persistence;

internal static class PersistenceDependencyInjection
{
    internal static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("AppDbConnectionString"));
            options.EnableDetailedErrors();
        });


        return services;
    }
}
