using System;
using System.Runtime.Serialization;

namespace SSar.Contexts.Common.Application.IntegrationEvents
{
    public interface IIntegrationEvent : IBusPublishable
    {
        Guid EventId { get; }
        int EventVersion { get; }
        DateTime OccurredAtUtc { get; }
        string Label { get; }
        string Publisher { get; }  // E.g. "SSar.Membership", for subscription filtering
    }
}