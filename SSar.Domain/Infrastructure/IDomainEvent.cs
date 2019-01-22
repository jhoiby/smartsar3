using System;

namespace SSar.Domain.Infrastructure
{
    public interface IDomainEvent : MediatR.INotification
    {
        Guid EventId { get; }
        int EventVersion { get; }
        DateTime OccurredAtUtc { get; }
    }
}