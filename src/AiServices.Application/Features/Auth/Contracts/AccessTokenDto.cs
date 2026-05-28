using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiServices.Application.Features.Auth.Contracts
{
    public sealed class AccessTokenDto
    {
        public string Token { get; init; } = string.Empty;

        public DateTime ExpiresAtUtc { get; init; }
    }

}
