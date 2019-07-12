using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CshTryouts
{
    public struct StringNum
    {
        public string Val { get; private set; }

        public StringNum(string val)
        {
            if(StringNum.Validate(val) )
                Val = val;
            else
            {
                throw new ArgumentException();
            }
        }

        public static StringNum operator +(StringNum a, StringNum b)
        {

        }

        public static bool Validate(string value)
        {
            foreach (var digit in value)
            {
                if (digit < 48 && digit > 87)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
