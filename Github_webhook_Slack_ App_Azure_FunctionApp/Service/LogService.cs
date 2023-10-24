using Github_webhook_Slack_App_Azure_FunctionApp.DAL;
using Github_webhook_Slack_App_Azure_FunctionApp.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Github_webhook_Slack_App_Azure_FunctionApp.Service
{
    public class LogService : ILogService
    {
        private readonly ILogger _logger;
        private readonly ILogRepo _logRepo;

        public LogService(ILogRepo logRepo, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<LogService>();
            _logRepo = logRepo;
        }

        public async Task<IEnumerable<GithubPayload>> GetAll()
        {
            return await _logRepo.GetAllAsync();
        }

        public async Task<GithubPayload?> GetById(string rowKey)
        {
            _logger.LogInformation($"Attempting to retrieve data by RowKey: {rowKey}");
            return await _logRepo.GetByRowKeyAsync(rowKey);
        }

        public async Task<IEnumerable<GithubPayload>> GetByRepo(string partitionKey)
        {
            _logger.LogInformation($"Attempting to retrieve data by PartitionKey: {partitionKey}");
            return await _logRepo.GetByPartitionKeyAsync(partitionKey);
        }

        public async Task InsertAsync(GithubPayload payload)
        {
            _logger.LogInformation("Attempting to log a git commit.");
            await _logRepo.InsertAsync(payload);
        }
    }
}
