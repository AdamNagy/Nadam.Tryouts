using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MongoDbPoc
{
    public interface IDbCollection<T>
    {
        Task Insert(T item);
        Task<IEnumerable<Ingestion>> Get();
    }

    public class IngestorCollection : IDbCollection<Ingestion>
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Ingestion> _collection;

        public IngestorCollection(string connectionString)
        {
            _mongoClient = new MongoClient(connectionString);
            _database = _mongoClient.GetDatabase("pds-local");
            _collection = _database.GetCollection<Ingestion>("initial");
        }

        public async Task Insert(Ingestion item)
        {
            await _collection.InsertOneAsync(item);
        }

        public async Task<IEnumerable<Ingestion>> Get()
        {
            var filterDef = new FilterDefinitionBuilder<Ingestion>();
            var filter = filterDef.In(x => x.Key, new[] {
                "e24f8043-cbe1-45d2-8261-6aec62ff9acf:240a87d0-da98-4ad8-959d-9b88921502c6",
                "15c22298-09b4-4dd8-838a-e02dfc776d68:11a15664-3905-4f39-9bc1-3f66805ccf55"});

            var findings = _collection.Find(filter).ToList();
            return findings;
        }
        
    }
}