# Github_webhook_Slack_App_Azure_FunctionApp
for this to work add local.settings.json with MyDatabaseConnection and MySlackURL
{
    "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "MyDatabaseConnection": "URL here or use UseDevelopmentStorage=true for local testing using azurite",
    "MySlackURL": "https://hooks.slack.com/services/xxxxxxxxxxx/xxxxxxxxxxx/xxxxxxxxxxxxxxxxxxxxxxx"
  }
}

for this to work you also have to set up a github webhook that makes a post everytime you push something, link this web hook to this azure function.
for local testing ngrok could be an option.

You also have to set up a slack app. Incoming Webhooks
Post messages from external sources into Slack.

this will give a url for you workspace in this format: https://hooks.slack.com/services/xxxxxxxxxxx/xxxxxxxxxxx/xxxxxxxxxxxxxxxxxxxxxxx

the json payload should be in this format:
{
  text: "something"
}
