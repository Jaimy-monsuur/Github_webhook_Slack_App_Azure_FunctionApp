using Microsoft.WindowsAzure.Storage.Table;

namespace Github_webhook_Slack_App_Azure_FunctionApp.Model
{
    public class GithubPayload : TableEntity
    {
        public string? repoName { get; set; }
        public string? repositoryId { get; set; }
        public string? branchName { get; set; }
        public string? commitId { get; set; }
        public string? committedBy { get; set; }
        public string? commitMessage { get; set; }
        public string? timestamp { get; set; }

        public GithubPayload() { }

        public GithubPayload(string? repoName, string? repositoryId, string? branchName, string? commitId, string? committedBy, string? commitMessage, string? timestamp)
        {
            this.repoName = repoName;
            this.repositoryId = repositoryId;
            this.branchName = branchName;
            this.commitId = commitId;
            this.committedBy = committedBy;
            this.commitMessage = commitMessage;
            this.timestamp = timestamp;

            RowKey = commitId; // identify by commitId 
            PartitionKey = repositoryId; //group by repoId, (mby also by branch)
        }
    }
}




