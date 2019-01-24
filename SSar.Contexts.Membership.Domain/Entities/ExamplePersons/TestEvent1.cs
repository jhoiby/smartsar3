using SSar.Contexts.Common.Domain.DomainEvents;

namespace SSar.Contexts.Membership.Domain.Entities.ExamplePersons
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
