using Github_webhook_Slack_App_Azure_FunctionApp.DAL;
using Github_webhook_Slack_App_Azure_FunctionApp.Model;
using Microsoft.Extensions.Logging;

namespace Github_webhook_Slack_App_Azure_FunctionApp.Service
{
    public class LogService : ILogService
    {
        private readonly ILogger _logger;

        private readonly ILogRepo _logRepo;

        public LogService(ILogRepo logRepo, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SlackService>();
            _logRepo = logRepo;
        }

        public async Task InsertAsync(Github_Payload payload)
        {
            _logger.LogInformation("Atempting to log a git commit.");
            await _logRepo.InsertAsync(payload);
        }
    }
}
