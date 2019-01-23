using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SSar.Application.IntegrationEvents;
using SSar.Domain.Membership.ExamplePersons;
using SSar.Infrastructure.IntegrationEvents;

namespace SSar.Application.DomainEventHandlers.Handlers
{
    [Serializable]
    public class ExamplePersonCreatedToIntegrationEventHandler : INotificationHandler<ExamplePersonCreated>
    {
        private IIntegrationEventQueue _integrationEventQueue;

        public ExamplePersonCreatedToIntegrationEventHandler(IIntegrationEventQueue integrationEventQueue)
        {
            _integrationEventQueue =
                integrationEventQueue ?? throw new ArgumentNullException(nameof(integrationEventQueue));
        }

        public Task Handle(ExamplePersonCreated notification, CancellationToken cancellationToken)
        {
            _integrationEventQueue.Add(
                new ExamplePersonCreatedIntegrationEvent(notification.Id, notification.Name,
                    notification.EmailAddress));

            return Task.CompletedTask;
        }
    }
}
