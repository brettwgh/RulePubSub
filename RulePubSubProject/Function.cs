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
    public class Function : IHttpFunction
    {
        private readonly string _topicName = "projects/geotab-soleng/topics/BrettTest";
        private readonly string _myKey = "keyItem";
        private readonly string _myKeyValue = "KeyValue";
        private readonly ILogger _logger;

        public Function(ILogger<Function> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task HandleAsync(HttpContext context)
        {
            _logger.LogInformation("starting execution...");
            // get the request object
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;

            _logger.LogInformation($"ContentType: {request.ContentType}");
            string topicMessage = "";
            if (request.ContentType.Contains("form"))
            {
                foreach (string key in request.Form.Keys)
                {
                    if (string.Equals(key, _myKey))
                    {
                        if (string.Equals(request.Form[key], _myKeyValue))
                        {
                            _logger.LogInformation($"Successfully authenticated...");
                            response.StatusCode = StatusCodes.Status200OK;
                        }
                        else
                        {
                            response.StatusCode = StatusCodes.Status401Unauthorized;
                        }
                    }
                    else
                    {
                        topicMessage += $"key: {key}, value: {request.Form[key]}\n";
                        _logger.LogInformation(topicMessage);
                    }
                }
            }
            //_logger.LogInformation("publishing message to topic...");
            //await _pubSub.SendMessageAsync(topicName, message);

            _logger.LogInformation("finishing execution...");

            //await context.Response.WriteAsync($"{responseMessage}");
            await response.CompleteAsync();
        }
    }
}
