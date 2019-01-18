using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SSar.Contexts.Common.Events;
using SSar.Contexts.Membership.Domain.AggregateRoots.ExamplePerson;

namespace SSar.Contexts.Membership.Application.EventHandlers
{
    public class PublishExamplePersonCreatedToIntegrationBus : INotificationHandler<ExamplePersonCreated>
    {
        private IIntegrationBusService _integrationBus;

        public PublishExamplePersonCreatedToIntegrationBus(IIntegrationBusService integrationBus)
        {
            _integrationBus = integrationBus ?? throw new ArgumentException(nameof(integrationBus));
        }

        public async Task Handle(ExamplePersonCreated notification, CancellationToken cancellationToken)
        {
            await _integrationBus.SendAsync(notification);
        }
    }
}
