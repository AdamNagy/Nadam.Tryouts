using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Logging;

[assembly: WebJobsStartup(typeof(GitApiIndexUpdater.Startup))]
namespace GitApiIndexUpdater
{
    public class Function1
    {
        public ICognitiveSearchService SearchService { get; set; }

        public Function1(ICognitiveSearchService searchService)
        {
            SearchService = searchService;
        }

        [FunctionName("UpdateGitApiIndex")]
        public async void Run(
            [TimerTrigger("0 5 * * * *")]TimerInfo myTimer,
            // [HttpTrigger(AuthorizationLevel.Anonymous, "update")]   // http://<APP_NAME>.azurewebsites.net/api/<FUNCTION_NAME>
            ILogger log)
        {
            await SearchService.UpdateIndex();
            log.LogInformation($"Functionexecuted at: {DateTime.Now}\n");
        }
    }
}
