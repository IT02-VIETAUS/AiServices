using AiServices.Application.Abstractions.Persistence.Dispatch;

namespace AiServices.Infrastructure.DatabaseContext.ApplicationDbs;

public partial class ApplicationDbContext :
    IDispatchReadDbContext,
    IDispatchWriteDbContext
{
}
