
using AiServices.Domain.Entities.AuditSchema;
using Microsoft.EntityFrameworkCore;

namespace AiServices.Infrastructure.DatabaseContext.ApplicationDbs
{
    public partial class ApplicationDbContext
    {
        public virtual DbSet<EventLog> EventLogs { get; set; } = default!;
        public virtual DbSet<CodeCounter> CodeCounters { get; set; } = default!;
        public virtual DbSet<AuditLog> AuditLogs { get; set; } = default!;
    }
}
