using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functional
{
    public delegate int GetInt(int value);
    public delegate string GetString(string value);

    public class Context
    {
        private int intValue;
        public int GetInt(int value)
        {
            intValue = value;
            return intValue;
        }
    }
}
