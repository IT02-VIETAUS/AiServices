namespace VA.AiApi.Auth;

public sealed class ErpUserContext
{
    public Guid CompanyId { get; init; }
    public Guid EmployeeId { get; init; }
    public string EmployeeName { get; init; } = string.Empty;
    public string PartCode { get; init; } = string.Empty;
    public IReadOnlyList<string> Roles { get; init; } = [];

    public bool HasRole(string role)
        => Roles.Any(x => string.Equals(x, role, StringComparison.OrdinalIgnoreCase));
}
