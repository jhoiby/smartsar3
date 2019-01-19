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
        string SourceAggregate { get; }

        // The last two are leaky abstractions but are useful for message bus subscription filtering.
        // Using other grouping/filtering methods would add another layer of complexity which
        // isn't desired right now.
    }
}