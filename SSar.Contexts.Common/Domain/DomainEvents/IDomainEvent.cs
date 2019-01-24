using System;

namespace SSar.Contexts.Common.Domain.DomainEvents
{
    public interface IDomainEvent : MediatR.INotification
    {
        Guid EventId { get; }
        int EventVersion { get; }
        DateTime OccurredAtUtc { get; }
    }
}