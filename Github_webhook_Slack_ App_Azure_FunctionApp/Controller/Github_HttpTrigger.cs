using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Github_webhook_Slack_App_Azure_FunctionApp.Model;
using Github_webhook_Slack_App_Azure_FunctionApp.Service;
using Github_webhook_Slack_App_Azure_FunctionApp.Utils;
using System.Net;

namespace Github_webhook_Slack_App_Azure_FunctionApp.Controller
{
    public class Github_HttpTrigger
    {
        private readonly ILogger _logger;
        private readonly ISlackService _slackService;
        private readonly ILogService _logService;

        public Github_HttpTrigger(ILoggerFactory loggerFactory, ISlackService slackService, ILogService logService)
        {
            _logger = loggerFactory.CreateLogger<Github_HttpTrigger>();
            _slackService = slackService;
            _logService = logService;
        }


        [Function("Github_HttpTrigger")]
        public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            _logger.LogInformation("HTTP trigger function processed a GitHub webhook request");

            string requestBody;

            using (StreamReader streamReader = new StreamReader(req.Body))
            {
                requestBody = streamReader.ReadToEnd();

                if (string.IsNullOrWhiteSpace(requestBody))
                {
                    _logger.LogInformation("Request body is empty or whitespace.");
                }
            }

            try
            {
                GithubPayload payload = DataMapper.MapJsonStringToGithub_Payload(requestBody);
                SlackPayload slack_Payload = DataMapper.MapGithubPayloadToSlackPayload(payload);

                await  _slackService.SendPayloadToSlack(slack_Payload);
                await  _logService.InsertAsync(payload);
                return req.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                var response = req.CreateResponse(HttpStatusCode.InternalServerError);
                response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                response.WriteString("Error");
                return response;
            }

        }
    }
}
