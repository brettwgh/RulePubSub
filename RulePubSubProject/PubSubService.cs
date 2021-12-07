using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace RulePubSubProject
{
    public class PubSubService
    {
        private readonly ILogger _logger;

        public PubSubService(ILogger<PubSubService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task SendMessageAsync(
            string topicNameString,
            string message)
        {
            _logger.LogDebug($"Publishing message to {topicNameString}...");

            TopicName topicName = TopicName.Parse(topicNameString);

            // Publish a message to the topic using PublisherClient.
            PublisherClient publisher = await PublisherClient.CreateAsync(topicName);

            // PublishAsync() has various overloads. Here we're using the string overload.
            string messageId = await publisher.PublishAsync($"{message}");

            _logger.LogDebug($"messageId: {messageId}, message sent: {message}");

            // PublisherClient instance should be shutdown after use.
            // The TimeSpan specifies for how long to attempt to publish locally queued messages.
            await publisher.ShutdownAsync(TimeSpan.FromSeconds(15));
        }
    }
}
