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

        public UnitTestingModelType()
        {
            var randomGenerator = new Random();
            IntTypeProp = randomGenerator.Next(10, 100);
            StringTypeProp = "Fusce tempus mauris tortor, eget tristique dui malesuada at. Sed et ante risus. Curabitur lacinia lacus in augue rutrum tincidunt. Donec molestie urna a turpis lobortis mattis sagittis maximus lacus. Suspendisse finibus elit hendrerit velit consequat, nec feugiat dui egestas. Sed ac justo malesuada, laoreet est at, auctor eros. Aliquam ultricies eget ex id laoreet.";
            DatetimeTypeProp = DateTime.Now.AddDays(randomGenerator.Next(100, 300));
        }
    }
}
