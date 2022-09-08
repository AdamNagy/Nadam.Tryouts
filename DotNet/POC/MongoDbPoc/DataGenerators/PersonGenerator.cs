using MongoDbPoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBenchmark.DataGenerators
{
    internal class PersonGenerator : IDataGenerator<Person>
    {
        public IEnumerable<Person> Generate()
        {
            throw new NotImplementedException();
        }
    }
}
