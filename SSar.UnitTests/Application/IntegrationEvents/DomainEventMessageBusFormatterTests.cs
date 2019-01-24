using System;
using System.Text;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using Shouldly;
using SSar.Contexts.Common.Application.IntegrationEvents;
using SSar.Contexts.Common.Data.ServiceInterfaces;
using SSar.Infrastructure;
using SSar.Infrastructure.ServiceBus;
using Xunit;

namespace SSar.UnitTests.Application.IntegrationEvents
{
    public class IntegrationEventAzureBusMessageBuilderTests
    {
        #region Arrange

        private readonly IntegrationEvent _integrationEvent;

        private class TestIntegrationEvent : IntegrationEvent
        {
            public TestIntegrationEvent(string someProperty)
            :base(ApplicationInfo.Name, nameof(TestIntegrationEvent))
            {
                SomeProperty = someProperty;
            }

            public string SomeProperty { get; }
        }

        public IntegrationEventAzureBusMessageBuilderTests()
        {
            _integrationEvent = new TestIntegrationEvent("Hello world");
        }

        #endregion

        [Fact]
        public void Build_returns_AzureServiceBusMessage()
        {
            var builder = new AzureIntegrationMessageBuilder();

            builder.WithObject(_integrationEvent);

            var message = builder.Build();

            message.ShouldBeOfType<Message>();
        }

        [Fact]
        public void WithObject_returns_this()
        {
            var builder = new AzureIntegrationMessageBuilder();

            var @return = builder.WithObject(_integrationEvent);

            @return.ShouldBe<IBusMessageBuilder<IIntegrationEvent,Message>>(builder);
        }

        [Fact]
        public void Build_returns_message_wrapping_provided_event()
        {
            var builder = new AzureIntegrationMessageBuilder();

            var message = builder
                .WithObject(_integrationEvent)
                .Build();

            dynamic deserializedEvent = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(message.Body));

            ((string) deserializedEvent["SomeProperty"]).ShouldBe("Hello world");
        }

        [Fact]
        public void Build_contains_message_with_expected_properties()
        {
            var builder = new AzureIntegrationMessageBuilder();

            var message = builder
                .WithObject(_integrationEvent)
                .Build();

            message.ShouldSatisfyAllConditions(
                () => message.ContentType.ShouldBe("application/json"),
                () => message.Label.ShouldBe("TestIntegrationEvent"),
                () => message.MessageId.ShouldNotBe(default(Guid).ToString()),
                () => message.UserProperties["Publisher"].ShouldBe(ApplicationInfo.Name));
        }

        [Fact]
        public void Build_throws_exception_if_object_not_provided()
        {
            var builder = new AzureIntegrationMessageBuilder();

            var ex = Should.Throw<InvalidOperationException>(() => builder.Build());
            ex.Message.ShouldBe("An object to be serialized must be provided to the WithObject method before calling build.");
        }
    }
}
