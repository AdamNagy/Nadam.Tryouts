using Nadam.Lib.JsonDb.Test.TestHelpers.SimpleDb.SimpleModel;
using System.Collections.Generic;

namespace Nadam.Lib.JsonDb.Test.TestHelpers.SimpleDb
{
    public class SimpleDbSeeder
    {
        public static SimpleJsonContext GetFilledDb()
        {
            var self = new SimpleDbSeeder();
            var context = new SimpleJsonContext("path=../../app_data/SimpleJson", false);

            return context;
        }

        public IEnumerable<DimensionA> SeedDimensionATable()
        {
            return new List<DimensionA>()
            {
                new DimensionA()
                {
                    Id = 1,
                    PropertyInt = 5,
                    PropertyString = "Dim 1 message 1"
                },
                new DimensionA()
                {
                    Id = 2,
                    PropertyInt = 6,
                    PropertyString = "Dim 1 message 2"
                },
                new DimensionA()
                {
                    Id = 3,
                    PropertyInt = 7,
                    PropertyString = "Dim 1 message 3"
                }
            };
        }

        public IEnumerable<DimensionB> SeedDimensionBTable()
        {
            return new List<DimensionB>()
            {
                new DimensionB()
                {
                    Id = 1,
                    PropertyInt = 5,
                    PropertyString = "Dim 2 message 1"
                },
                new DimensionB()
                {
                    Id = 2,
                    PropertyInt = 6,
                    PropertyString = "Dim 2 message 2"
                },
                new DimensionB()
                {
                    Id = 3,
                    PropertyInt = 7,
                    PropertyString = "Dim 2 message 3"
                }
            };
        }

        public IEnumerable<DimensionC> SeedDimensionCTable()
        {
            return new List<DimensionC>()
            {
                new DimensionC()
                {
                    Id = 1,
                    PropertyInt = 5,
                    PropertyString = "Dim 3 message 1"
                },
                new DimensionC()
                {
                    Id = 2,
                    PropertyInt = 6,
                    PropertyString = "Dim 3 message 2"
                },
                new DimensionC()
                {
                    Id = 3,
                    PropertyInt = 7,
                    PropertyString = "Dim 3 message 3"
                }
            };
        }
    }
}
