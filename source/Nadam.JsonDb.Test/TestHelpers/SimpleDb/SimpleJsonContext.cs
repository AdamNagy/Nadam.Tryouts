using Nadam.Lib.JsonDb.Test.TestHelpers.SimpleDb.SimpleModel;
using System.Collections.Generic;
<<<<<<< HEAD
using Nadam.Global.JsonDb;
=======
using Nadam.JsonDb;
>>>>>>> master

namespace Nadam.Lib.JsonDb.Test.TestHelpers.SimpleDb
{
    public class SimpleJsonContext : JsonDbEngineContext
    {
        public SimpleJsonContext(string path, bool inmemory) : base(path, inmemory) { }
        public SimpleJsonContext(string path) : base(path) { }

        public List<DimensionA> DimensionA { get; set; }
        public List<DimensionB> DimensionB { get; set; }
        public List<DimensionC> DimensionC { get; set; }
    }
}
