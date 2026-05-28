using AiServices.Application.Abstractions.Authentication;
using AiServices.Application.Abstractions.Security;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiServices.Application.Features.Auth.UseCases.Logout
{
    public sealed record LogoutCommand : IRequest<Unit>;

    internal sealed class LogoutCommandHandler : IRequestHandler<LogoutCommand, Unit>
    {
        private readonly IIdentityAuthenticationService _identityAuthenticationService;
        private readonly ICurrentUser _currentUser;

        public LogoutCommandHandler(
            IIdentityAuthenticationService identityAuthenticationService,
            ICurrentUser currentUser)
        {
            _identityAuthenticationService = identityAuthenticationService;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            await _identityAuthenticationService.RevokeRefreshTokenAsync(_currentUser.UserId, cancellationToken);
            return Unit.Value;
        }
    }

}
