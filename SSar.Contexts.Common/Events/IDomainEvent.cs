using System;

namespace SSar.Contexts.Common.Events
{
    public interface IDomainEvent : MediatR.INotification
    {
        Guid EventId { get; }
        int EventVersion { get; }
        DateTime OccurredAtUtc { get; }
        string Label { get; }             // For integration service bus Message.Label
        string Publisher { get; }         // Name of application or BC, i.e. "Membership"
    }
}