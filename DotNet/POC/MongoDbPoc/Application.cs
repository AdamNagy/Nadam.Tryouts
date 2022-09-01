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
        public Application()
        {
        }

        public async Task Run()
        {

        }
    }
}
