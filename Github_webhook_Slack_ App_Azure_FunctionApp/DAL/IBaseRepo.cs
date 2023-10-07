using Microsoft.WindowsAzure.Storage.Table;

namespace Github_webhook_Slack_App_Azure_FunctionApp.DAL
{
    public interface IBaseRepo<T> where T : TableEntity
    {
        Task InsertAsync(T entity); // only need to insert for this azure function
    }
}
