using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SSar.Contexts.Common.Application.RequestPipelineBehaviors
{
    public class RequestLoggerBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _logger;

        public RequestLoggerBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // TODO: Add additional log info such as correlation IDs, user name, etc.
            
            var name = typeof(TRequest).Name;
            _logger.LogInformation("NEW REQUEST: {Name} {@Request}", name, request);
            

            return await next();
        }
    }
}
