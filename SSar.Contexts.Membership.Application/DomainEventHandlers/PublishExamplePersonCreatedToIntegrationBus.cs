using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Azure.ServiceBus;
using SSar.Contexts.Membership.Domain.DomainEvents;

namespace SSar.Contexts.Membership.Application.DomainEventHandlers
{
    public class PublishExamplePersonCreatedToIntegrationBus : INotificationHandler<ExamplePersonCreated>
    {
        private IQueueClient _integrationBus;

        public PublishExamplePersonCreatedToIntegrationBus(IQueueClient integrationBus)
        {
            _integrationBus = integrationBus ?? throw new ArgumentException(nameof(integrationBus));
        }

        public async Task Handle(ExamplePersonCreated notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine("*** Domain event handler fired: PublishExamplePersonCreatedToIntegrationBus (ExamplePersonCreated)");

            // TODO: Refactor this into an integration bus service as it will be repeated a lot
            var messageBody = 
                $"New ExamplePerson created (initial event test message). Name: {notification.ExamplePerson.Name} , Email: {notification.ExamplePerson.EmailAddress}";
            var message = new Message(Encoding.UTF8.GetBytes(messageBody));

            await _integrationBus.SendAsync(message);

            Debug.WriteLine("*** ExamplePersonCreated string message published to Azure Service Bus");
        }
    }
}
