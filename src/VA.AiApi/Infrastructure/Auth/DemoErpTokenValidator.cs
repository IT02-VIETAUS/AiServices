using Microsoft.Extensions.Options;
using VA.AiApi.Auth;
using VA.AiApi.Options;

namespace VA.AiApi.Infrastructure.Auth;

public sealed class DemoErpTokenValidator(IOptions<ErpAuthOptions> options) : IErpTokenValidator
{
    public Task<ErpUserContext?> ValidateAsync(string token, CancellationToken cancellationToken)
    {
        var cfg = options.Value;

        // Phase 1: demo validator.
        // Sau này thay class này bằng validator thật dùng ERP token/session hiện có.
        if (!string.Equals(token, cfg.DemoToken, StringComparison.Ordinal))
        {
            return Task.FromResult<ErpUserContext?>(null);
        }

        _ = Guid.TryParse(cfg.DemoCompanyId, out var companyId);
        _ = Guid.TryParse(cfg.DemoEmployeeId, out var employeeId);

        var user = new ErpUserContext
        {
            CompanyId = companyId == Guid.Empty ? Guid.Parse("00000000-0000-0000-0000-000000000001") : companyId,
            EmployeeId = employeeId == Guid.Empty ? Guid.Parse("00000000-0000-0000-0000-000000000002") : employeeId,
            EmployeeName = cfg.DemoEmployeeName,
            PartCode = cfg.DemoPartCode,
            Roles = cfg.DemoRoles
        };

        return Task.FromResult<ErpUserContext?>(user);
    }
}
