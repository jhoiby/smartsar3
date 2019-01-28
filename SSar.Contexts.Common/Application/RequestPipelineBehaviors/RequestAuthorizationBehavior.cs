using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SSar.Contexts.Common.Application.Authorization;

namespace SSar.Contexts.Common.Application.RequestPipelineBehaviors
{
    public class RequestAuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ILogger _logger;

            public RequestAuthorizationBehavior(IAuthorizationService authorizationService, ILogger<TRequest> logger)
            {
                _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            }

            public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
            {
                var authResult = _authorizationService.Authorize();

                _logger.LogDebug("AuthorizationResult: {authResult}", authResult);

                return await next();
            }
    }
}
