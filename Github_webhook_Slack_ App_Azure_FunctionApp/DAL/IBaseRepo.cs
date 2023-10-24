using Microsoft.WindowsAzure.Storage.Table;

namespace Github_webhook_Slack_App_Azure_FunctionApp.DAL
{
    public interface IBaseRepo<T> where T : TableEntity
    {
        Task InsertAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetByPartitionKeyAsync(string partitionKey); //repo id
        Task<T?> GetByRowKeyAsync(string rowKey); //commit id
    }
}
