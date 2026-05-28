using System.Security.Claims;

namespace VA.AiApi.Auth;

public sealed class HttpErpUserContextAccessor(IHttpContextAccessor httpContextAccessor) : IErpUserContextAccessor
{
    public ErpUserContext GetRequired()
    {
        var user = httpContextAccessor.HttpContext?.User;
        if (user?.Identity?.IsAuthenticated != true)
        {
            throw new UnauthorizedAccessException("ERP user context is not authenticated.");
        }

        var companyIdText = user.FindFirstValue("company_id");
        var employeeIdText = user.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(companyIdText, out var companyId))
        {
            throw new UnauthorizedAccessException("Invalid company id in ERP token.");
        }

        if (!Guid.TryParse(employeeIdText, out var employeeId))
        {
            throw new UnauthorizedAccessException("Invalid employee id in ERP token.");
        }

        return new ErpUserContext
        {
            CompanyId = companyId,
            EmployeeId = employeeId,
            EmployeeName = user.FindFirstValue("employee_name") ?? string.Empty,
            PartCode = user.FindFirstValue("part_code") ?? string.Empty,
            Roles = user.FindAll(ClaimTypes.Role).Select(x => x.Value).Distinct().ToArray()
        };
    }
}
