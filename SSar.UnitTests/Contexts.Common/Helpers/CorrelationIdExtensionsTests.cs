using System;
using Shouldly;
using SSar.Contexts.Common.Helpers;
using Xunit;

namespace SSar.UnitTests.Contexts.Common.Helpers
{
    public class CorrelationIdExtensionsTests
    {
        [Fact]
        public void Correlation_id_should_be_first_8_characters_of_guid()
        {
            var id = Guid.Parse("02807C1F-A783-4ECE-B944-0D80E0341B2E");

            var correlationId = id.ToCorrelationId();

            correlationId.ShouldBe("02807C1F", Case.Insensitive);
        }
    }
}
