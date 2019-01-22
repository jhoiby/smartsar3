using SSar.Domain.Infrastructure;

namespace SSar.Domain.Membership.ExamplePersons
{
    public class TestEvent2 : DomainEvent
    {
        public TestEvent2()
        {
        }

        public string Property1 => "Property 1 string";
        public string Property2 => "Property 2 string";
    }
}
