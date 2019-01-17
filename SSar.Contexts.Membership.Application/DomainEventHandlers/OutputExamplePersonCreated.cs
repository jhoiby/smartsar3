using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SSar.Contexts.Membership.Domain.DomainEvents;

namespace SSar.Contexts.Membership.Application.DomainEventHandlers
{
    public class WriteExamplePersonCreatedToDebug : INotificationHandler<ExamplePersonCreated>
    {
        public async Task Handle(ExamplePersonCreated notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine("\n*** ExamplePersonCreated event received\n");
        }
    }
}
