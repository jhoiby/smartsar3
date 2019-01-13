using System;
using System.Collections.Generic;
using System.Text;
using SSar.Contexts.Common.AbstractClasses;

namespace SSar.Contexts.Common.Notifications
{
    public class AggregateResult<TAggregate> where TAggregate : IAggregateRoot
    {
        public TAggregate Aggregate => throw new NotImplementedException();
    }
}
