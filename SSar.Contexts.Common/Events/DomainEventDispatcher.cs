using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SSar.Contexts.Common.Entities;

namespace SSar.Contexts.Common.Events
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IMediator _mediator;
        private readonly IServiceBusSender _integrationBus;

        public DomainEventDispatcher(IMediator mediator, IServiceBusSender integrationBus)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _integrationBus = integrationBus ?? throw new ArgumentNullException(nameof(integrationBus));
        }

        public async Task<IAggregateRoot[]> DispatchInternalBoundedContextEventsAsync(IAggregateRoot[] aggregates)
        {
            foreach (var aggregate in aggregates)
            {
                var events = aggregate.Events.ToArray();
                foreach (var domainEvent in events)
                {
                    await _mediator.Publish(domainEvent);
                }
            }

            return aggregates;
        }

        public IAggregateRoot[] ClearEventEntities(IAggregateRoot[] aggregates)
        {
            foreach (var aggregate in aggregates)
            {
                aggregate.Events.Clear();
            }

            return aggregates;
        }
    }
}
