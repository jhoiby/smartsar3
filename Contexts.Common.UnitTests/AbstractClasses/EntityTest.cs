using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using SSar.Contexts.Common.AbstractClasses;
using Xunit;

namespace SSar.Contexts.Common.UnitTests.AbstractClasses
{
    public class EntityTest
    {
        // Entity is abstract, requiring concrete implementation to test
        private class ConcreteEntity : Entity
        {
        }

        [Fact]
        public void Id_property_is_set_to_non_default_on_construction()
        {
            var concreteEntity = new ConcreteEntity();

            concreteEntity.Id.ShouldNotBe(Guid.Empty);
        }
    }
}
