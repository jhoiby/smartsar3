using System.Buffers.Text;
using SSar.Contexts.Common.Events;

namespace SSar.Contexts.Membership.Domain.AggregateRoots.ExamplePerson
{
    public class TestEvent2 : DomainEvent
    {
        public TestEvent2() : base(BoundedContextInfo.Name, nameof(ExamplePerson), nameof(TestEvent2))
        {
        }

        public string Property1 => "Property 1 string";
        public string Property2 => "Property 2 string";
    }
}
