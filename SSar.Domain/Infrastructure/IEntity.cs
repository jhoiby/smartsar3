using System;

namespace SSar.Domain.Infrastructure
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}