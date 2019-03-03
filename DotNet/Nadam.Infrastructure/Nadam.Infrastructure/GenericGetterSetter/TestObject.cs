using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nadam.Infrastructure.GenericGetterSetter
{
    public class TestObject
    {
        public int TestProperty { get; set; }   // works with generic setters and setters
        public int TestVariable;    // not working with generic setters and setters
    }
}
