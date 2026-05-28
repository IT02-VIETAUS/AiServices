using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AiServices.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        // Giai đoạn 2 sẽ đăng ký PostgreSQL/pgvector repositories tại đây.
        // Ví dụ sau này: VectorStoreRepository, ChatLogRepository, AgentLogRepository.
        _ = configuration;
        return services;
    }
}
