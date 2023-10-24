using System.Net;
using Github_webhook_Slack_App_Azure_FunctionApp.Service;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Github_webhook_Slack_App_Azure_FunctionApp.Controller
{
    public class GetAll_HttpTrigger
    {
        private readonly ILogger _logger;
        private readonly ILogService _logService;

        public GetAll_HttpTrigger(ILoggerFactory loggerFactory, ILogService logService)
        {
            _logger = loggerFactory.CreateLogger<GetAll_HttpTrigger>();
            _logService = logService;
        }

        [Function("GetAll_HttpTrigger")]
        public async Task<HttpResponseData> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetAll")] HttpRequestData req)
        {
            _logger.LogInformation("HTTP trigger function processed a GetAll request");

            try
            {
                var githubPayloads = await _logService.GetAll();

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
