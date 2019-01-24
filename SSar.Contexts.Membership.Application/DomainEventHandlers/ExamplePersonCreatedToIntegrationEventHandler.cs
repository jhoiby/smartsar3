using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SSar.Contexts.Common.Application.ServiceInterfaces;
using SSar.Contexts.Membership.Application.IntegrationEvents;
using SSar.Contexts.Membership.Domain.Entities.ExamplePersons;

namespace SSar.Contexts.Membership.Application.DomainEventHandlers
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
