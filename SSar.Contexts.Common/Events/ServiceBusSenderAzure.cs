using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;

namespace SSar.Contexts.Common.Events
{
    // TODO: Currently this will send events over integration bus prior
    // TODO: to db commit! Setup queuing flow and dispatch integration events
    // TODO: only after successful db commit. (See the DbContext class to fix)

    public class ServiceBusSenderAzure : IServiceBusSender
    {
        private readonly ITopicClient _topicClient;

        public AzureServiceBusSender(ITopicClient topicClient)
        {
            _topicClient = topicClient ?? throw new ArgumentNullException(nameof(topicClient));
        }

        public async Task SendAsync(IIntegrationEvent @event)
        {
            var message =
                new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event)))
                {
                    ContentType = "application/json",
                    Label = @event.Label,
                    MessageId = Guid.NewGuid().ToString()
                };

            // Additional properties for use with subscription message filtering
            message.UserProperties.Add("Publisher", @event.Publisher); // E.g. "SSar.Membership"

            await _topicClient.SendAsync(message);
        }
    }
}
