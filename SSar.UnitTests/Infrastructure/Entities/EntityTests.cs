using System;
using Shouldly;
using SSar.Contexts.Common.Domain.Entities;
using Xunit;

namespace SSar.UnitTests.Infrastructure.Entities
{
    public class EntityTests
    {
        // Entity is abstract, requiring concrete implementation to test
        private class ConcreteEntity : Entity
        {
        }

        [Fact]
        public void Id_property_should_be_set_to_non_default_on_construction()
        {
            var concreteEntity = new ConcreteEntity();

            concreteEntity.Id.ShouldNotBe(Guid.Empty);
        }
    }
}
