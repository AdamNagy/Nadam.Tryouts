using System;
using System.Threading.Tasks;

namespace MongoDbPoc
{
    public interface IApplication
    {
        Task Run();
    }

    public class Application : IApplication
    {
        private readonly IDbCollection<Ingestion> _ingestionCollection;
        private readonly DataFeeder<Ingestion> _ingestionFeeder;

        public Application(
            IDbCollection<Ingestion> ingestionCollection,
            DataFeeder<Ingestion> ingestionFeeder)
        {
            _ingestionCollection = ingestionCollection;
            _ingestionFeeder = ingestionFeeder;
        }

        public async Task Run()
        {
            await Task.Run(async () => {
                // await _ingestionFeeder.Feed(1000);
                var ingestions = await _ingestionCollection.Get();
                foreach (var item in ingestions)
                    Console.WriteLine(item);

                Console.WriteLine("Done");
                Console.ReadKey();
            });
        }
    }
}
