using System.Net;
using Azure;
using Github_webhook_Slack_App_Azure_FunctionApp.Service;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Github_webhook_Slack_App_Azure_FunctionApp.Controller
{
    public class GetById_HttpTrigger
    {
        private readonly ILogger _logger;

        private readonly IGithubLogService _logService;

        public GetById_HttpTrigger(ILoggerFactory loggerFactory, IGithubLogService logService)
        {
            _logger = loggerFactory.CreateLogger<GetById_HttpTrigger>();
            _logService = logService;
        }

        [Function("GetById_HttpTrigger")]
        public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetById/{rowKey}")] HttpRequestData req,
            string rowKey)
        {
            _logger.LogInformation("HTTP trigger function processed a GetById request");

            try
            {
                var githubPayload = await _logService.GetById(rowKey);

                if (githubPayload != null)
                {
                    var response = req.CreateResponse(HttpStatusCode.OK);
                    response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                    var payloadJson = JsonConvert.SerializeObject(githubPayload);
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
