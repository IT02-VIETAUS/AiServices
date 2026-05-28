using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace VA.AiApi.Auth;

public sealed class ErpTokenAuthenticationHandler(
    IOptionsMonitor<ErpTokenAuthenticationOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder,
    ISystemClock clock,
    IErpTokenValidator tokenValidator)
    : AuthenticationHandler<ErpTokenAuthenticationOptions>(options, logger, encoder, clock)
{
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authHeader = Request.Headers["Authorization"].ToString();
        if (string.IsNullOrWhiteSpace(authHeader))
        {
            return AuthenticateResult.Fail("Missing Authorization header.");
        }

        const string bearerPrefix = "Bearer ";
        if (!authHeader.StartsWith(bearerPrefix, StringComparison.OrdinalIgnoreCase))
        {
            return AuthenticateResult.Fail("Authorization header must use Bearer token.");
        }

        var token = authHeader[bearerPrefix.Length..].Trim();
        if (string.IsNullOrWhiteSpace(token))
        {
            return AuthenticateResult.Fail("Empty ERP token.");
        }

        var erpUser = await tokenValidator.ValidateAsync(token, Context.RequestAborted);
        if (erpUser is null)
        {
            return AuthenticateResult.Fail("Invalid ERP token.");
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, erpUser.EmployeeId.ToString()),
            new(ClaimTypes.Name, erpUser.EmployeeName),
            new("employee_name", erpUser.EmployeeName),
            new("company_id", erpUser.CompanyId.ToString()),
            new("part_code", erpUser.PartCode)
        };

        claims.AddRange(erpUser.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var identity = new ClaimsIdentity(claims, ErpTokenAuthenticationDefaults.SchemeName);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, ErpTokenAuthenticationDefaults.SchemeName);

        return AuthenticateResult.Success(ticket);
    }
}
