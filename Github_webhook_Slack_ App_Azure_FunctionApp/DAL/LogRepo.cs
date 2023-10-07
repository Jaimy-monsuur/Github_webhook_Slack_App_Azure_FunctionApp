using Github_webhook_Slack_App_Azure_FunctionApp.Model;

namespace Github_webhook_Slack_App_Azure_FunctionApp.DAL
{
    public class LogRepo : BaseRepo<Github_Payload>, ILogRepo
    {
        public LogRepo() : base()
        {
            _table = _tableClient.GetTableReference("GithubLogs");
            _table.CreateIfNotExistsAsync().GetAwaiter().GetResult();
        }
    }
}
