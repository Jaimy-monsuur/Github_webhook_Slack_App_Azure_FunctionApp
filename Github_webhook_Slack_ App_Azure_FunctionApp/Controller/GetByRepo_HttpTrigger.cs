using System.Net;
using Github_webhook_Slack_App_Azure_FunctionApp.Service;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Github_webhook_Slack_App_Azure_FunctionApp.Controller
{
    public class GetByRepo_HttpTrigger
    {
        private readonly ILogger _logger;
        private readonly IGithubLogService _logService;

        public GetByRepo_HttpTrigger(ILoggerFactory loggerFactory, IGithubLogService logService)
        {
            _logger = loggerFactory.CreateLogger<GetByRepo_HttpTrigger>();
            _logService = logService;
        }

        [Function("GetByRepo_HttpTrigger")]
        public async Task<HttpResponseData> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetByRepo/{partitionKey}")] HttpRequestData req,
            string partitionKey)
        {
            _logger.LogInformation("HTTP trigger function processed a GetByRepo request");

            try
            {
                var githubPayloads = await _logService.GetByRepo(partitionKey);

                if (githubPayloads != null)
                {
                    var response = req.CreateResponse(HttpStatusCode.OK);
                    response.Headers.Add("Content-Type", "application/json; charset=utf-8");
                    var payloadJson = JsonConvert.SerializeObject(githubPayloads);
                    response.WriteString(payloadJson);
                    return response;
                }
                else
                {
                    return req.CreateResponse(HttpStatusCode.NotFound);
                }
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
