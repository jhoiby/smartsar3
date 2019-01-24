using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SSar.Contexts.Common.Data.ServiceInterfaces;
using SSar.Contexts.Common.Domain.Entities;
using SSar.Contexts.Common.Domain.ServiceInterfaces;

namespace SSar.Infrastructure.DomainEventDispatch
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

        public async Task<IAggregateRoot[]> DispatchAndClearDomainEventsAsync(IAggregateRoot[] aggregates)
        {
            foreach (var aggregate in aggregates)
            {
                var events = aggregate.Events.ToArray();
                foreach (var domainEvent in events)
                {
                    await _mediator.Publish(domainEvent);
                }

                aggregate.Events.Clear();
            }

            return aggregates;
        }
    }
}
