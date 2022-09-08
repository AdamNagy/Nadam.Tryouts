using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDbPoc
{
    public interface IDocumentDbContext
    {
        // IMongoDatabase GetDatabase();
        IMongoCollection<BsonDocument> GetCollection(string collectionName);
        IMongoCollection<T> GetCollection<T>(string collectionName);
    }

    internal class MongoDbContext : IDocumentDbContext
    {
        private readonly string _databaseName;
        private readonly string _connectionString;

        public MongoDbContext(string connectionString, string dbName)
        {
            _connectionString = connectionString;
            _databaseName = dbName;
        }

        private IMongoDatabase GetDatabase()
        {
            var client = new MongoClient(_connectionString);
            return client.GetDatabase(_databaseName);
        }

        public IMongoCollection<BsonDocument> GetCollection(string collectionName)
        {
            var db = GetDatabase();
            return db.GetCollection<BsonDocument>(collectionName);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            var db = GetDatabase();
            return db.GetCollection<T>(collectionName);
        }
    }
}
