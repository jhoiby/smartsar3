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

        public DomainEventDispatcher(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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
