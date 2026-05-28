using AiServices.Domain.Entities.HrSchema;
using Microsoft.EntityFrameworkCore;

namespace AiServices.Application.Abstractions.Persistence.Hr;

public interface IHrDbContext
{
    DbSet<Employee> Employees { get; }

    DbSet<Part> Parts { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
