using SSar.Contexts.Common.Events;

namespace SSar.Contexts.Membership.Domain.AggregateRoots.ExamplePerson
{
    public class TestEvent1 : DomainEvent
    {
        public TestEvent1()
        {
        }

        public string Property1 => "Property 1 string";
        public string Property2 => "Property 2 string";
    }
}
