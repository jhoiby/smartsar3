using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using SSar.Infrastructure.IntegrationEvents;

namespace SSar.Infrastructure.ServiceBus
{
    // TODO: Currently this will send events over integration bus prior
    // TODO: to db commit! Setup queuing flow and dispatch integration events
    // TODO: only after successful db commit. (See the DbContext class to fix)

    public class ServiceBusSenderAzure : IServiceBusSender
    {
        private readonly ITopicClient _topicClient;
        private readonly IBusMessageBuilder<IIntegrationEvent, Message> _messageBuilder;

        public ServiceBusSenderAzure(
            ITopicClient topicClient, 
            IBusMessageBuilder<IIntegrationEvent, Message> messageBuilder)
        {
            _topicClient = topicClient ?? throw new ArgumentNullException(nameof(topicClient));
            _messageBuilder = messageBuilder ?? throw new ArgumentNullException(nameof(messageBuilder));
        }

        public async Task SendAsync(IIntegrationEvent @event)
        {
            var message = _messageBuilder.WithObject(@event).Build();

            //var message =
            //    new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event)))
            //    {
            //        ContentType = "hi", //"application/json",
            //        Label = "labe",// @event.Label,
            //        MessageId = default(Guid).ToString() //Guid.NewGuid().ToString()
            //        // TODO: Implement expiration date
            //    };

            //// Additional properties for use with subscription message filtering
            //message.UserProperties.Add("Publisher", @event.Publisher); // E.g. "SSar.Membership"

            await _topicClient.SendAsync(message);
        }
    }
}
