using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadamLib.Tests.TestModels
{
    public class UnitTestingModelType
    {
        // Number types
        public int IntTypeProp { get; set; }
        public decimal DecimalTypeProp { get; set; }

        // string and character
        public string StringTypeProp { get; set; }

        // Other
        public DateTime DatetimeTypeProp { get; set; }

        // Matrix
        public IList<IList<int>> intMatrix { get; set; }
        public IList<IList<object>> objectMatrix { get; set; }
    }
}
