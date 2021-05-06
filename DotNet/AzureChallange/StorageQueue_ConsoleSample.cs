using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace QueueApp
{
    class Program
    {
        private const string ConnectionString  = "DefaultEndpointsProtocol=https;EndpointSuffix=core.windows.net;AccountName=nadamqueuestorage0322;AccountKey=dX9iWwMviQ3vZJs5xA1T16f0uJftYCc+6p5WOGLS1atzu7a71dbmKpZfGVK1keVzgiUdcqqgxBSS6VDxg1SMiw==";
        private static CloudStorageAccount account = null;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            if( args.Length > 0 ) 
            {
                await SendArticleAsync(String.Join(' ', args));
                Console.WriteLine(args);
            }
            else
            {
                string value = await ReceiveArticleAsync();
                Console.WriteLine($"Received {value}");
            }
        }

        static async Task SendArticleAsync(string newsMessage)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference("newsqueue");
            await queue.CreateIfNotExistsAsync();

            var message = new CloudQueueMessage(newsMessage);
            await queue.AddMessageAsync(message);
        }

        static async Task<string> ReceiveArticleAsync()
        {
            CloudQueue queue = GetQueue();
            bool exists = await queue.ExistsAsync();
            if (exists)
            {
                CloudQueueMessage retrievedArticle = await queue.GetMessageAsync();
                if (retrievedArticle != null)
                {
                    string newsMessage = retrievedArticle.AsString;
                    await queue.DeleteMessageAsync(retrievedArticle);
                    return newsMessage;
                }
            }

            return "<queue empty or not created>";
        }

        static CloudQueue GetQueue()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            return queueClient.GetQueueReference("newsqueue");
        }
    }
}
