using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbPoc
{
    public interface DataFeeder<T>
    {
        Task Feed(int numberToFeed);
    }

    public class IngstionFeeder : DataFeeder<Ingestion>
    {
        private readonly IDbCollection<Ingestion> _collection;
        private readonly string[] _datapartitions = { "parition-1", "parition-2", "parition-3", "parition-4", "parition-5" };
        private readonly string[] _objectTypes = { "type-1", "type-2", "type-3", "type-4", "type-5", "type-6", "type-7" };
        private readonly string[] _statuses = { "started", "inProg", "error", "fatal", "changed", "iddle" };

        public IngstionFeeder(IDbCollection<Ingestion> collection)
        {
            _collection = collection;
        }

        public async Task Feed(int numberToFeed = 100)
        {
            foreach (var item in Generate(numberToFeed))            
                await _collection.Insert(item);            
        }

        private IEnumerable<Ingestion> Generate(int number = 100)
        {
            var rand = new Random();
            var currentDate = DateTime.Now;

            for (int i = 0; i < number; i++)
            {
                yield return new Ingestion()
                {
                    Attempts = rand.Next(1, 5),
                    Changed = currentDate.AddDays(rand.Next(1, 100) * -1),
                    Created = currentDate.AddDays(rand.Next(1, 100) * -1),
                    DataPartition = _datapartitions[rand.Next(_datapartitions.Length-1)],
                    Id = $"{Guid.NewGuid()}:{Guid.NewGuid()}",
                    ObjectId = Guid.NewGuid(),
                    ObjectType = _objectTypes[rand.Next(_objectTypes.Length-1)],
                    RevisionId = rand.Next(int.MaxValue),
                    Status = _statuses[rand.Next(_statuses.Length-1)],
                };
            }

            yield break;
        }
    }
}
