using Github_webhook_Slack_App_Azure_FunctionApp.Model;
using Newtonsoft.Json;

namespace Github_webhook_Slack_App_Azure_FunctionApp.Utils
{
    public class DataMapper
    {
        public static List<GithubPayload> MapJsonStringToGithub_Payload(string json)
        {
            dynamic payload = JsonConvert.DeserializeObject(json);
            string repoName = payload.repository.name;
            string repositoryId = payload.repository.id;
            string reference = payload.@ref;
            string branchName = reference.Split('/').Last().ToString();

            List<GithubPayload> githubPayloads = new List<GithubPayload>();

            foreach (var commit in payload.commits)
            {
                string commitId = commit.id;
                string committedBy = commit.author.name;
                string commitMessage = commit.message;
                string timestamp = commit.timestamp;

                githubPayloads.Add(new GithubPayload(repoName, repositoryId, branchName, commitId, committedBy, commitMessage, timestamp));
            }

            return githubPayloads;
        }

        public static List<SlackPayload> MapGithubPayloadToSlackPayload(List<GithubPayload> githubPayloads)
        {
            List<SlackPayload> slackPayloads = new List<SlackPayload>();

            foreach (var githubPayload in githubPayloads)
            {
                slackPayloads.Add(new SlackPayload(
                    githubPayload.repoName,
                    githubPayload.branchName,
                    githubPayload.committedBy,
                    githubPayload.commitMessage,
                    githubPayload.timestamp
                ));
            }

            return slackPayloads;
        }
    }
}
