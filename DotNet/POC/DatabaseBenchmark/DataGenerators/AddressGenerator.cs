using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseBenchmark.Extensions;
using MongoDbPoc.Models;

namespace DatabaseBenchmark.DataGenerators
{
    internal class AddressGenerator : IDataGenerator<Address>
    {
        private readonly IDataProvider _testDataProvider;

        public AddressGenerator(IDataProvider dataGenerator)
        {
            _testDataProvider = dataGenerator;
        }

        public static List<string> LocationTypes
            = new List<string>() { "street", "road", "avenue", "square", "ring" };

        public IEnumerable<string> GenerateZipCodes()
            => Enumerable.Range(1000, 9999).Select(x => x.ToString());

        public IEnumerable<string> GenerateStreets()
        {
            var loremIpsum = _testDataProvider.GetTestData("loremipsum1");

            var random = new Random();

            foreach (var item in loremIpsum)
            {
                var splitted = item.Split(" ");
                foreach (var location in splitted)
                {
                    var locationName = location.Trim().Trim('.').Trim(',').Trim(';');
                    locationName += " ";
                    locationName += LocationTypes[random.Next(0, LocationTypes.Count - 1)];

                    yield return locationName;
                }
            }
        }

        public IEnumerable<Address> Generate()
        {
            var cities = _testDataProvider.GetTestData("cities");
            var countries = _testDataProvider.GetTestData("countries").ToList();
            var streets = GenerateStreets();

            var random = new Random();

            int countryIndex = 0;

            var addresses = new List<Address>();
            foreach(var cityChunk in cities.Split(countries.Count()))
            {
                foreach (var city in cityChunk)
                {
                    addresses.Add(new Address()
                    {
                        RecId = Guid.NewGuid().ToString(),
                        Country = countries[countryIndex],
                        City = city,
                    });
                }

                ++countryIndex;
                if (countryIndex >= countries.Count - 1)
                    break;
            }

            foreach (var countryCity in addresses)
            {
                foreach (var item in streets)
                {
                    yield return new Address()
                    {
                        RecId = Guid.NewGuid().ToString(),
                        Country = countryCity.Country,
                        City = countryCity.City,
                        AddressLine1 = item,
                        AddressLine2 = random.Next(1, 100).ToString(),
                        ZipCode = random.Next(1000, 9999).ToString(),
                    };
                }
            }
        }
    }
}
