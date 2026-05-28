using AiServices.Application.Abstractions.Persistence.Employees;

namespace AiServices.Infrastructure.DatabaseContext.ApplicationDbs
{
    public partial class ApplicationDbContext :
        IEmployeeReadDbContext,
        IEmployeeManagementDbContext
    {
    }

}
