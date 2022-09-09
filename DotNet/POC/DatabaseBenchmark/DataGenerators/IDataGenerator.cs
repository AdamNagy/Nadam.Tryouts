using System.Collections.Generic;

namespace DatabaseBenchmark.DataGenerators
{
    public interface IDataGenerator<T>
    {
        public IEnumerable<T> Generate();
    }
}
