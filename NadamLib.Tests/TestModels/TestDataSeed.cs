using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadamLib.Tests.TestModels
{
    public class TestDataSeed
    {
        // Number types
        public int IntTypeProp() {
            return 42;
        }

        public double DoubleTypeprop() {
            return 42.42;
        }
        public decimal DecimalTypeProp() {
            return 68.23M;
        }

        // string and character
        public string StringTypeProp() {
            return "Hello world";
        }
        public char CharTypeProp() {
            return 'K';
        }
        public byte ByteTypeProp() {
            return 69;
        }
        public byte[] ByteArrTypeProp() {
            return new byte[] { 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };
        }

        // Other
        public DateTime DatetimeTypeProp() {
            return new DateTime(1989, 1, 30, 17, 25, 23);
        }
        public bool BoolTypeProp() {
            return true;
        }

        // IEnumerables
        public IEnumerable<int> IntEnumeratorTypeProp() {
            return new List<int>()
            {
                1,2,3,4,5,6
            };
        }
        public IEnumerable<string> StringEnumeratorTypeProp() {
            return new List<string>()
            {
                "Hello",
                "World",
                "Of",
                "Programming",
                "C#!"
            };
        }
        public IEnumerable<DateTime> DateTimeEnumeratorTypeProp() {
            return new List<DateTime>()
            {
                new DateTime(1985, 10, 25),
                new DateTime(2017, 1, 25),
                new DateTime(1211, 9, 20)
            };
        }
    }
}
