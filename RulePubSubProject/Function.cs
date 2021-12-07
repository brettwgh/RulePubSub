using Google.Cloud.Functions.Framework;
using Google.Cloud.Functions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace RulePubSubProject
{
    [FunctionsStartup(typeof(Startup))]
    public class Function : IHttpFunction
    {
        private readonly string _topicName = "projects/geotab-soleng/topics/BrettTest";
        private readonly string _myKey = "keyItem";
        private readonly string _myKeyValue = "KeyValue";
        private readonly ILogger _logger;
        private readonly PubSubService _pubSubService;

        public Function(
            ILogger<Function> logger,
            PubSubService pubSubService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _pubSubService = pubSubService;
        }

        public async Task HandleAsync(HttpContext context)
        {
            _logger.LogInformation("starting execution...");

            // get the request object
            HttpRequest request = context.Request;
            // get the response object
            HttpResponse response = context.Response;

            string topicMessage = "";
            if (request.ContentType.Contains("form"))
            {
                string authKey = request.Form[_myKey];
                if (string.IsNullOrEmpty(authKey))
                {
                    response.StatusCode = StatusCodes.Status401Unauthorized;
                    topicMessage = "";
                }
                else if (!string.Equals(request.Form[_myKey],_myKeyValue))
                {
                    response.StatusCode = StatusCodes.Status401Unauthorized;
                    topicMessage = "";
                }
                else
                {
                    foreach (string key in request.Form.Keys)
                    {
                        topicMessage += $"key: {key}, value: {request.Form[key]}\n";
                        _logger.LogDebug(topicMessage);
                    }

                    _logger.LogInformation("publishing message to topic...");
                    await _pubSubService.SendMessageAsync(_topicName, topicMessage);

                    response.StatusCode = StatusCodes.Status200OK;
                }
            }
            
            _logger.LogInformation("finishing execution...");

            await response.WriteAsync($"{topicMessage}");
        }
    }
}
