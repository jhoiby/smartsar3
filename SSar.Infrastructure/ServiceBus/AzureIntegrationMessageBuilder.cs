using System;
using System.Text;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using SSar.Contexts.Common.Application.IntegrationEvents;
using SSar.Contexts.Common.Data.ServiceInterfaces;

namespace SSar.Infrastructure.ServiceBus
{
    public class AzureIntegrationMessageBuilder : IBusMessageBuilder<IIntegrationEvent, Microsoft.Azure.ServiceBus.Message>
    {
        private IIntegrationEvent _event;

        public IBusMessageBuilder<IIntegrationEvent, Message> WithObject(IIntegrationEvent @event)
        {
            _event = @event;

            return this;
        }

        public Message Build()
        {
            _event = _event ?? throw new InvalidOperationException(
                         "An object to be serialized must be provided to the WithObject method before calling build.");

            var message =
                new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_event)))
                {
                    ContentType = "application/json",
                    Label = _event.Label,
                    MessageId = Guid.NewGuid().ToString()
                        // TODO: Implement expiration date
                    };

            // Additional properties for use with subscription message filtering
            message.UserProperties.Add("Publisher", _event.Publisher); // E.g. "SSar.Membership"

            return message;
        }

        //public AzureIntegrationMessageBuilder WithObject(IIntegrationEvent @event)
        //{
        //    _event = @event;

        //    return this;
        //}

        //public Message Build()
        //{
        //    _event = _event ?? throw new InvalidOperationException(
        //                 "An object to be serialized must be provided to the WithObject method before calling build.");

        //    var message =
        //        new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_event)))
        //        {
        //            ContentType = "application/json",
        //            Label = _event.Label,
        //            MessageId = Guid.NewGuid().ToString()
        //            // TODO: Implement expiration date
        //        };

        //    // Additional properties for use with subscription message filtering
        //    message.UserProperties.Add("Publisher", _event.Publisher); // E.g. "SSar.Membership"

        //    return message;
        //}
    }
}
