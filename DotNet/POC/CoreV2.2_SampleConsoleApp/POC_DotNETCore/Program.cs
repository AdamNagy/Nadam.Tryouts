using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using B_Project;

namespace POC_DotNETCore
{
    class Program
    {
        public static ServiceProvider container;
        public static IConfigurationRoot config;

        static void Main(string[] args)
        {
            container = new ServiceCollection()
                .AddScoped<App>()
                .AddScoped<Dependency>()
                .BuildServiceProvider();

            container.GetService<App>()
                .Run();

            //config = new ConfigurationBuilder()
            //     // .SetBasePath(Directory.GetCurrentDirectory())
            //     .AddJsonFile("appsettings.json")
            //     .Build();

            var cObj = new CRepository().Get();
        }
    }

    public class App
    {
        public Dependency Dependency { get; }

        public void Run()
        {
            Console.WriteLine("Hello Core");
        }

        public App(Dependency dependency)
        {
            Dependency = dependency;
        }
    }

    public class Dependency
    {

    }
}
