using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiServices.Application.Features.Auth.Contracts
{
    public sealed class RefreshTokenRequestDto
    {
        public string RefreshToken { get; init; } = string.Empty;
    }
}
