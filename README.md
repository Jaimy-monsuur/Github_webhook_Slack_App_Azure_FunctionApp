# Assignment 3: SlackChannel


## Jaimy Monsuur

**GitHub link:**

**[https://github.com/Jaimy-monsuur/Github_webhook_Slack_App_Azure_FunctionApp](https://github.com/Jaimy-monsuur/Github_webhook_Slack_App_Azure_FunctionApp)**

**Slack app**



Store image on your image server and adjust path/filename/extension if necessary.


![alt_text](images/image6.png "image_tooltip")


Create a new slack app and select from** From scratch. **And add it to your workspace.


![alt_text](images/image4.png "image_tooltip")


Select incoming webhook and add it to your workspace. You will get a secret key that you can use for the slack api. https://hooks.slack.com/services/xxxxxxxxxxx/xxxxxxxxxxx/xxxxxxxxxxxxxxxxxxxxxxx



**Github**



![alt_text](images/image7.png "image_tooltip")


Create a github webhook and set the azure function url as the payload url. I used ngrok to test it.

** \
Application Set up**

For this application to work add the following to the local.settings.json(or add in azure configuration):


```
{
    "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
    "MyDatabaseConnection": "UseDevelopmentStorage=true", //URL here or use UseDevelopmentStorage=true for local testing using azurite
    "MySlackURL": "https://hooks.slack.com/services/xxxxxxxxxxx/xxxxxxxxxxx/xxxxxxxxxxxxxxxxxxxxxxx"
  }
}
```


**Results API**



![alt_text](images/image5.png "image_tooltip")

![alt_text](images/image1.png "image_tooltip")



**Results slack**



![alt_text](images/image2.png "image_tooltip")


**Results table storage**



Store image on your image server and adjust path/filename/extension if necessary.


![alt_text](images/image3.png "image_tooltip")

