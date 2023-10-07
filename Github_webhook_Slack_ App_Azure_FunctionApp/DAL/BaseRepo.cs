using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Github_webhook_Slack_App_Azure_FunctionApp.DAL
{
    public class BaseRepo<T> : IBaseRepo<T> where T : TableEntity, new()
    {
        private readonly CloudStorageAccount _storageAccount;
        protected CloudTableClient _tableClient;
        protected CloudTable _table;

        public BaseRepo() {
            string? connectionString = Environment.GetEnvironmentVariable("MyDatabaseConnection");
            _storageAccount = CloudStorageAccount.Parse(connectionString);
            _tableClient = _storageAccount.CreateCloudTableClient();
        }

        public async Task InsertAsync(T entity)
        {
            TableOperation insertOperation = TableOperation.Insert(entity);
            await _table.ExecuteAsync(insertOperation);
        }
    }
}
