using Github_webhook_Slack_App_Azure_FunctionApp.Model;

namespace Github_webhook_Slack_App_Azure_FunctionApp.DAL
{
    public interface ILogRepo : IBaseRepo<GithubPayload> { } // for thing specificly for GithubPayload object, and for injecting it
}
