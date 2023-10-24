using Github_webhook_Slack_App_Azure_FunctionApp.Model;

namespace Github_webhook_Slack_App_Azure_FunctionApp.Service
{
    public interface ILogService
    {
        Task InsertAsync(List<GithubPayload> payload);
        Task<IEnumerable<GithubPayload>> GetAll();
        Task<IEnumerable<GithubPayload>> GetByRepo(string partitionKey); //GetByPartitionKeyAsync
        Task<GithubPayload?> GetById(string id); //GetByRowKeyAsync
    }
}
