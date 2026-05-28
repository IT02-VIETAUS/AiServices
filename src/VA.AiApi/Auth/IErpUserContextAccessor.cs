namespace VA.AiApi.Auth;

public interface IErpUserContextAccessor
{
    ErpUserContext GetRequired();
}
