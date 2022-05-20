using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksIo
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<string> task = Task.Factory.StartNew<string>
                (() => GetPosts("https://jsonplaceholder.typicode.com/posts"));

            SomethingElse();
            
            try
            {
                //task.Wait();
                Console.WriteLine(task.Result);
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private static void SomethingElse()
        {
            //Dummy implementation
        }

        private static string GetPosts(string url)
        {
            throw null;
            using (var client = new System.Net.WebClient())
            {
                return client.DownloadString(url);
            }
        }
    }
}
