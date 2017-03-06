using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadamLib.Tests.TestModels
{
    public class UnitTestingModelBase
    {
        // Number types
        public int IntTypeProp { get; set; }
        public double DoubleTypeprop { get; set; }
        public decimal DecimalTypeProp { get; set; }

        // string and character
        public string StringTypeProp { get; set; }
        public char CharTypeProp { get; set; }
        public byte ByteTypeProp { get; set; }
        public byte[] ByteArrTypeProp { get; set; }
        
        // Other
        public DateTime DatetimeTypeProp { get; set; }
        public bool BoolTypeProp { get; set; }

        // IEnumerables
        public IEnumerable<int> IntEnumeratorTypeProp { get; set; }
        public IEnumerable<string> StringEnumeratorTypeProp { get; set; }
        public IEnumerable<DateTime> DateTimeEnumeratorTypeProp { get; set; }

        // ILists
        public IList<int> IntListTypeProp { get; set; }
        public IList<string> StringListTypeProp { get; set; }
        public IList<DateTime> DateTimeListTypeProp { get; set; }

        // Dictionary
        public Dictionary<int, string> IntStringDictionaryTypeProp { get; set; }
        public Dictionary<string, DateTime> StringDateTimeDictionaryTypeProp { get; set; }
        public Dictionary<DateTime, bool> DateTimeBoolDictionaryTypeProp { get; set; }

        //
        public IEnumerable<UnitTestingModelType> ComplexTypeList { get; set; }

        public UnitTestingModelBase(bool seedData = true)
        {
            if (seedData)
            {
                var dataSeeder = new TestDataSeed();

                //Number tyspes
                IntTypeProp = dataSeeder.IntTypeProp();
                DoubleTypeprop = dataSeeder.DoubleTypeprop();
                DecimalTypeProp = dataSeeder.DecimalTypeProp();

                // string and character
                StringTypeProp = dataSeeder.StringTypeProp();
                CharTypeProp = dataSeeder.CharTypeProp();
                ByteTypeProp = dataSeeder.ByteTypeProp();
                ByteArrTypeProp = dataSeeder.ByteArrTypeProp();
            }
        }
    }
}
