namespace VA.AiApi.Auth;

public interface IErpTokenValidator
{
    Task<ErpUserContext?> ValidateAsync(string token, CancellationToken cancellationToken);
}
