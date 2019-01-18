using System;

namespace SSar.Contexts.Common.Events
{
    public interface IDomainEvent : MediatR.INotification
    {
        Guid EventId { get; }
        int EventVersion { get; }
        DateTime OccurredAt { get; }
        string Label { get; }
    }
}