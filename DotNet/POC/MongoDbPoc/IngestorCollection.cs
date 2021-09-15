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
            _database = _mongoClient.GetDatabase("pds");
            _collection = _database.GetCollection<Ingestion>("Ingestion");
        }

        public async Task Insert(Ingestion item)
        {
            await _collection.InsertOneAsync(item);
        }

        public async Task<IEnumerable<Ingestion>> Get()
            => (await _collection.FindAsync(p => p.DataPartition == "parition-1")).ToList();
        
    }
}