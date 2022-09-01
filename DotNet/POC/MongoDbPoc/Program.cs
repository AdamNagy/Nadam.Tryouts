using System;
using System.Configuration;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MongoDbPoc
{
    class Program
    {        
        static async Task Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            // Application code should start here.
            await host.Services.GetRequiredService<IApplication>().Run();

            await host.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, configuration) => {
                    configuration.Sources.Clear();

                    IHostEnvironment env = hostingContext.HostingEnvironment;

                    configuration
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);

                    IConfigurationRoot configurationRoot = configuration.Build();
                })
            .ConfigureServices((hostingContext, services) => {
                //services
                //    .AddSingleton<IApplication, Application>()
                //    .AddSingleton<IDbCollection<Ingestion>>(container => 
                //        new IngestorCollection(hostingContext.Configuration.GetSection("ConnectionStrings")["mongo"])
                //    )
                //    .AddTransient<DataFeeder<Ingestion>, IngstionFeeder>();
            });
    }
}
