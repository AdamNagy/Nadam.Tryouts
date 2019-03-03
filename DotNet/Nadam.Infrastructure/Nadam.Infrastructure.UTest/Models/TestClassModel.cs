using System;

namespace Nadam.Infrastructure.UTest
{
    class TestClassModel : Entity
    {
        public int intProp { get; set; }
        public string str { get; set; }
        public DateTime datetime { get; set; }

        public TestClassModel()
        {
            Init(this);
        }
    }
}
