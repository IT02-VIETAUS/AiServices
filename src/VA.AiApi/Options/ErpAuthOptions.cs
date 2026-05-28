namespace VA.AiApi.Options;

public sealed class ErpAuthOptions
{
    public const string SectionName = "ErpAuth";

    // Phase 1 dùng Demo để dựng khung trước.
    // Phase sau thay DemoErpTokenValidator bằng validator gọi ERP Auth/API hoặc đọc DB phiên đăng nhập.
    public string Mode { get; set; } = "Demo";
    public string DemoToken { get; set; } = "dev-erp-token";
    public string DemoCompanyId { get; set; } = "00000000-0000-0000-0000-000000000001";
    public string DemoEmployeeId { get; set; } = "00000000-0000-0000-0000-000000000002";
    public string DemoEmployeeName { get; set; } = "Developer";
    public string DemoPartCode { get; set; } = "IT";
    public string[] DemoRoles { get; set; } = ["SysAdmin"];
}
