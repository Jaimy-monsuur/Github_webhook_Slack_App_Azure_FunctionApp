using Github_webhook_Slack_App_Azure_FunctionApp.Model;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Github_webhook_Slack_App_Azure_FunctionApp.Service
{
    public class SlackService : ISlackService
    {
        private readonly ILogger _logger;

        public SlackService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SlackService>(); 
        }

        public async Task SendPayloadToSlack(List<SlackPayload> payloads)
        {
            string? url = Environment.GetEnvironmentVariable("MySlackURL");

            using (HttpClient httpClient = new HttpClient())// disposed of HttpClient when it's no longer needed.
            {
                foreach (SlackPayload payload in payloads)
                {
                    var content = new StringContent(payload.ToJson(), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        _logger.LogInformation("Payload sent to Slack successfully.");
                    }
                    else
                    {
                        _logger.LogError($"Failed to send payload to Slack. Status code: {response.StatusCode}");
                    }
                }
            }
        }
    }
}
