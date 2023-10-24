using Newtonsoft.Json;

namespace Github_webhook_Slack_App_Azure_FunctionApp.Model
{
    public class SlackPayload
    {
        public string? repoName { get; set; }
        public string? branchName { get; set; }
        public string? committedBy { get; set; }
        public string? commitMessage { get; set; }
        public string? timestamp { get; set; }

        public SlackPayload(string? repoName, string? branchName, string? committedBy, string? commitMessage, string? timestamp)
        {
            this.repoName = repoName;
            this.branchName = branchName;
            this.committedBy = committedBy;
            this.commitMessage = commitMessage;
            this.timestamp = timestamp;
        }

        public override string ToString()
        {
            return $"Commit was made to {repoName} under the branch: {branchName}." +
                   $"\nThe commit was done by {committedBy}." +
                   $"\nMessage: {commitMessage}" +
                   $"\nTimestamp: {timestamp}.";
        }

        public string ToJson()
        {
            var json = new
            {
                text = this.ToString()
            };

            return JsonConvert.SerializeObject(json);
        }
    }
}
