using Google.Cloud.Functions.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace RulePubSubProject
{
    public class Function : IHttpFunction
    {
        string topicName = "projects/geotab-soleng/topics/BrettTest";
        private readonly ILogger _logger;

        public Function(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task HandleAsync(HttpContext context)
        {
            _logger.LogInformation("starting execution...");
            HttpRequest request = context.Request;
            _logger.LogInformation($"ContentType: {request.ContentType}");
            string message = "";
            string response = "not found";
            if (request.ContentType.Contains("form"))
            {
                foreach (string key in request.Form.Keys)
                {
                    message += $"key: {key}, value: {request.Form[key]}\n";
                    _logger.LogInformation(message);
                }
            }
            _logger.LogInformation("publishing message to topic...");
            //await _pubSub.SendMessageAsync(topicName, message);

            _logger.LogInformation("finishing execution...");
            _logger.LogDebug($"response: {response}");
            await context.Response.WriteAsync($"{response}");
        }
    }
}
