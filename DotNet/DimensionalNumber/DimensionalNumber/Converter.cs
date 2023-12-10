using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimensionalNumberLib
{
    public struct Converter
    {
        public Measurement A { get; private set; }
        public Measurement B { get; private set; }

        public Double Change { get; private set; }
    }
}
