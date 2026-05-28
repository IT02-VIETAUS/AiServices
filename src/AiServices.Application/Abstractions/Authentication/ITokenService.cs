using AiServices.Application.Features.Auth.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiServices.Application.Abstractions.Authentication
{
    public interface ITokenService
    {
        AccessTokenDto CreateAccessToken(AuthenticatedUserDto user);
        string CreateRefreshToken();
    }
}
