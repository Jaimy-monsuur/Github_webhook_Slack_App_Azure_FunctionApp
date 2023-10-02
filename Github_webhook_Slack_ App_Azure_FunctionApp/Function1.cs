using System.Diagnostics;
using System.Net;
using Azure.Core;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using static System.Net.WebRequestMethods;
using System.Net.Http.Headers;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Github_webhook_Slack__App_Azure_FunctionApp
{
    public class Function1
    {
        private readonly ILogger _logger;
        static readonly HttpClient httpClient = new HttpClient();


        public Function1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
        }

        [Function("Function1")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("HTTP trigger function processed a GitHub webhook request");

            var response = req.CreateResponse(HttpStatusCode.OK);
            // get payload
            string requestBody = new StreamReader(req.Body).ReadToEnd();
            dynamic payload = JsonConvert.DeserializeObject(requestBody);

            // Check if payload is null before logging
            if (payload != null)
            {
                // Log the payload as a JSON string 
                _logger.LogInformation("not empty");
            }
            else
            {
                // Log a message indicating that the payload is null
                _logger.LogInformation("Payload is null.");
            }

            // get commit details required to call API
            var commitId = payload.after;
            var message = payload.head_commit.message;
            var owner = payload.head_commit.author.username;
            var repo = payload.repository.name;
            var time = payload.head_commit.timestamp;


            //Post to URL 
            httpClient.BaseAddress = new Uri("https://hooks.slack.com/services/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"); // Replace with your target URL
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var newPayload = new
            {
                text = $"There was a new commit to {repo}, by user: {owner}. Message: {message}, Timestamp: {time}."
            };

            string jsonPayload = JsonConvert.SerializeObject(newPayload); // If using JSON payload
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json"); // If using JSON payload
            var slackResponse = httpClient.PostAsync("", content);

            // respond
            response.WriteString($"We had a commit.");

            return response;
        }
    }
}
