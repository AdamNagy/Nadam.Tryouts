using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using DatabaseBenchmark.DataGenerators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDbPoc.Models;

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
                var currentDir = Directory.GetCurrentDirectory();
                services
                    .AddSingleton<IApplication, Application>()
                    .AddScoped<IDataProvider, DataProvider>(container => new DataProvider($"{currentDir}/SampleData"))
                    .AddScoped<IDataGenerator<Address>, AddressGenerator>()
                    .AddScoped<IDataGenerator<Person>, PersonGenerator>()
                    .AddSingleton<IDocumentDbContext>(container =>
                        new MongoDbContext(hostingContext.Configuration.GetSection("ConnectionStrings")["mongo"], "People"));
            });
    }
}
