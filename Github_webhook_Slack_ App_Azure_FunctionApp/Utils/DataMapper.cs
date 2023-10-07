using Github_webhook_Slack_App_Azure_FunctionApp.Model;
using Newtonsoft.Json;

namespace Github_webhook_Slack_App_Azure_FunctionApp.Utils
{
    public class DataMapper
    {
        public static Github_Payload MapJsonStringToGithub_Payload(string json)
        {
            dynamic payload = JsonConvert.DeserializeObject(json);
            string repoName = payload.repository.name;
            string repositoryId = payload.repository.id;
            string reference = payload.@ref;
            string branchName = reference.Split('/').Last().ToString();
            string commitId = payload.head_commit.id;
            string committedBy = payload.head_commit.author.name;
            string commitMessage = payload.head_commit.message;
            string timestamp = payload.head_commit.timestamp;

            return new Github_Payload(repoName, repositoryId, branchName, commitId, committedBy, commitMessage, timestamp);
        }

        public static Slack_Payload MapGithubPayloadToSlackPayload(Github_Payload githubPayload)
        {
            return new Slack_Payload(
                githubPayload.repoName,
                githubPayload.branchName,
                githubPayload.committedBy,
                githubPayload.commitMessage,
                githubPayload.timestamp
            );
        }
    }
}
