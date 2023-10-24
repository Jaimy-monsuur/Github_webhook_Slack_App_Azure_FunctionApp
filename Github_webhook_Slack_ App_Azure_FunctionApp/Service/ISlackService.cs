using Github_webhook_Slack_App_Azure_FunctionApp.Model;

namespace Github_webhook_Slack_App_Azure_FunctionApp.Service
{
    public interface ISlackService
    {
        Task SendPayloadToSlack(List<SlackPayload> payload);
    }
}
