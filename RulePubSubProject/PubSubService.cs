using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace RulePubSubProject
{
    /// <summary>
    /// The Google Pub/Sub service module.
    /// </summary>
    public class PubSubService
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="logger">Injected logging service.</param>
        public PubSubService(ILogger<PubSubService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// A single message publication instance to the topic defined.
        /// </summary>
        /// <param name="topicPathString">The required pub/sub topic path string.</param>
        /// <param name="message">The string message to publish.</param>
        /// <returns>An asynchronous task.</returns>
        public async Task SendMessageAsync(
            string topicPathString,
            string message)
        {
            _logger.LogDebug($"Publishing message to {topicPathString}...");
            TopicName topicName = TopicName.Parse(topicPathString);

            // Publish a message to the topic using PublisherClient.
            PublisherClient publisher = await PublisherClient.CreateAsync(topicName);

            // PublishAsync() has various overloads. Here we're using the string overload.
            string messageId = await publisher.PublishAsync($"{message}");

            _logger.LogDebug($"Published message - messageId: {messageId}, message: {message}");

            // PublisherClient instance should be shutdown after use.
            // The TimeSpan specifies for how long to attempt to publish locally queued messages.
            await publisher.ShutdownAsync(TimeSpan.FromSeconds(15));
        }
    }
}
