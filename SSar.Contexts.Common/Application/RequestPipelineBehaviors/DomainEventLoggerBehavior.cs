using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace SSar.Contexts.Common.Application.RequestPipelineBehaviors
{
    public class DomainEventLoggerBehavior<TNotification> : IRequestPreProcessor<TNotification>
    {
        private readonly ILogger _logger;

        public DomainEventLoggerBehavior(ILogger<TNotification> logger)
        {
            _logger = logger;
        }

        public Task Process(TNotification request, CancellationToken cancellationToken)
        {
            var name = typeof(TNotification).Name;
            _logger.LogInformation("NEW DOMAIN EVENT: {Name} {@Request}", name, request);

            return Task.CompletedTask;
        }
    }
}
