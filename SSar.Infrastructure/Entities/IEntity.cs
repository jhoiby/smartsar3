using System;

namespace SSar.Infrastructure.Entities
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}