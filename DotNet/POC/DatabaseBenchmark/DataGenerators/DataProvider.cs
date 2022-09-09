using System;
using System.Collections.Generic;
using System.IO;

namespace DatabaseBenchmark.DataGenerators
{
    public interface IDataProvider
    {
        IEnumerable<string> GetTestData(string fileName);
    }

    internal class DataProvider : IDataProvider
    {
        private readonly string _testDataDir;

        public DataProvider(string root)
        {
            _testDataDir = root;
        }

        public IEnumerable<string> GetTestData(string fileName)
            => File.ReadAllLines($"{_testDataDir}/{fileName}.txt");
    }
}
