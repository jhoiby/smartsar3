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
    // TODO: only after successful db commit.

    public class IntegrationBusService : IIntegrationBusService
    {
        private readonly ITopicClient _topicClient;

        public IntegrationBusService(ITopicClient topicClient)
        {
            _topicClient = topicClient ?? throw new ArgumentNullException(nameof(topicClient));
        }

        public async Task SendAsync(IDomainEvent @event)
        {
            var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event)))
            {
                // TODO: IS THIS THE RIGHT LABEL? HOW DO I RECONSTRUCT EXAMPLEPERSON?

                ContentType = "application/json",
                Label = @event.Label,
                MessageId = @event.EventId.ToString(),
            };

            message.UserProperties.Add("Publisher", @event.Publisher);

            await _topicClient.SendAsync(message);
        }
    }
}
