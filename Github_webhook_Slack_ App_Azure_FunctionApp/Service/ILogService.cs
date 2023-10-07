using Github_webhook_Slack_App_Azure_FunctionApp.Model;

namespace Github_webhook_Slack_App_Azure_FunctionApp.Service
{
    public interface ILogService
    {
        Task InsertAsync(Github_Payload payload);

    }
}
