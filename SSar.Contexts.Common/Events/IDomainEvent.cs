using System;

namespace SSar.Contexts.Common.Events
{
    public interface IDomainEvent : MediatR.INotification
    {
        Guid EventId { get; }
        int EventVersion { get; }
        DateTime OccurredAtUtc { get; }
        string Label { get; }
        string Publisher { get; }
    }
}