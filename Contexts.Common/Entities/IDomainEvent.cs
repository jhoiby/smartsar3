using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace SSar.Contexts.Entities.DomainEvents
{
    public interface IDomainEvent : INotification
    {
        string Description { get; }
    }
}