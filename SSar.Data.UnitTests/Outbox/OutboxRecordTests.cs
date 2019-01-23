using SSar.Data.Outbox;
using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using Xunit;

namespace SSar.Data.UnitTests.Outbox
{
    public class OutboxRecordTests
    {
        [Fact]
        public void Constructor_should_set_properties()
        {
            var id = Guid.NewGuid();
            var objectType = "SSar.Domain.Application.DomainEvents.SomeRandomEvent";
            var serializedObject = "SomeBunchOfData";
            var validUntil = DateTime.Now.AddMinutes(10);

            var record = new OutboxPackage(id, objectType, serializedObject, validUntil);

            record.ShouldSatisfyAllConditions(
                () => record.PackageId.ShouldBe(id),
                () => record.ObjectType.ShouldBe(objectType),
                () => record.SerializedObject.ShouldBe(serializedObject),
                () => record.Created.ShouldBe(DateTime.Now, TimeSpan.FromSeconds(10)),
                () => record.ValidUntil.ShouldBe(validUntil));
        }
    }
}
