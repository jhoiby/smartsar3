using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SSar.Domain.Membership.ExamplePersons;

namespace SSar.Application.DomainEventHandlers.Handlers
{
    public class WriteExamplePersonCreatedToDebug : INotificationHandler<ExamplePersonCreated>
    {
        public async Task Handle(ExamplePersonCreated notification, CancellationToken cancellationToken)
        {
            await Task.Run( () =>
                Debug.WriteLine("*** Domain event handler fired: WriteExamplePersonCreatedToDebug (ExamplePersonCreated)"));
        }
    }
}
