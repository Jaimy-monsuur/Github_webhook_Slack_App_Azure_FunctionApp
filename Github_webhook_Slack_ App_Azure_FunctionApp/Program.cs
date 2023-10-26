using Github_webhook_Slack_App_Azure_FunctionApp.DAL;
using Github_webhook_Slack_App_Azure_FunctionApp.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(configureServices =>
    {
       configureServices.AddTransient<IGithubLogService, GithubLogService>();
       configureServices.AddTransient<ISlackService, SlackService>();
       configureServices.AddTransient<ILogRepo, LogRepo>();
    })
    .Build();

host.Run();
