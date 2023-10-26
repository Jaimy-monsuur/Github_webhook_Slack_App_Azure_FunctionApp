using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Github_webhook_Slack_App_Azure_FunctionApp.DAL
{
    public class BaseRepo<T> : IBaseRepo<T> where T : TableEntity, new()
    {
        private readonly CloudStorageAccount _storageAccount;
        protected CloudTableClient _tableClient;
        protected CloudTable _table;

        public BaseRepo()
        {
            string? connectionString = Environment.GetEnvironmentVariable("MyDatabaseConnection");
            _storageAccount = CloudStorageAccount.Parse(connectionString);
            _tableClient = _storageAccount.CreateCloudTableClient();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            List<T> entities = new List<T>();
            TableQuery<T> query = new TableQuery<T>();

            foreach (T entity in _table.ExecuteQuerySegmentedAsync(query, null).Result)
            {
                entities.Add(entity);
            }

            return entities;
        }

        public async Task<IEnumerable<T>> GetByPartitionKeyAsync(string partitionKey)
        {
            TableQuery<T> query = new TableQuery<T>().Where(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));

            var entities = await _table.ExecuteQuerySegmentedAsync(query, null);
            return entities.Results;
        }

        public async Task<T?> GetByRowKeyAsync(string rowKey)
        {
            TableQuery<T> query = new TableQuery<T>().Where(
                TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, rowKey));

            var entities = await _table.ExecuteQuerySegmentedAsync(query, null);
            return entities.FirstOrDefault();
        }

        public async Task InsertAsync(T entity)
        {
            TableOperation insertOperation = TableOperation.Insert(entity);
            await _table.ExecuteAsync(insertOperation);
        }
    }
}
