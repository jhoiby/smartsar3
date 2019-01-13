using System;
using System.Collections.Generic;
using System.Text;
using SSar.Contexts.Common.AbstractClasses;

namespace SSar.Contexts.Common.Notifications
{
    public class AggregateResult<TAggregate> where TAggregate : IAggregateRoot
    {
        private readonly TAggregate _aggregate;

        private AggregateResult(TAggregate aggregate)
        {
            _aggregate = aggregate;
        }

        public TAggregate Aggregate => _aggregate;

        public static AggregateResult<TAggregate> FromAggregate(TAggregate aggregate)
        {
            return new AggregateResult<TAggregate>(aggregate);
        }
    }
}
