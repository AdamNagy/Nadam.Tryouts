using DatabaseBenchmark.DataGenerators;
using MongoDbPoc.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbPoc
{
    public interface IApplication
    {
        Task Run();
    }

    internal class Application : IApplication
    {
        private readonly IDataGenerator<Address> _addressGenerator;

        public Application(IDataGenerator<Address> addressGenerator)
        {
            _addressGenerator = addressGenerator;
        }

        public async Task Run()
        {
            foreach (var address in _addressGenerator.Generate().Take(10))
            {
                Console.WriteLine(address);
            }
        }
    }
}
