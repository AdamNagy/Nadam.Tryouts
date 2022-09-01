using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDbPoc
{
    internal class DocumentRepository<T>
    {
        public DocumentRepository()
        {
            var client = new MongoClient("mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass&directConnection=true&ssl=false");
            var database = client.GetDatabase("demo");
            var collection = database.GetCollection<BsonDocument>("bar");
        }


    }
}
