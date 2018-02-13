using System.Collections.Generic;
using Nadam.Global.JsonDb;

namespace Nadam.ConsoleTest.MIV
{
    class MivExtensionDbContext : JsonDbEngineContext
    {
        public MivExtensionDbContext(bool inmemory = true) : base("MivJsonDbExtension", inmemory) {}

        public List<SequenceExtension> SequenceExtensions { get; set; }
        public List<Place> Places { get; set; }
        public List<HighHeel> HighHeels { get; set; }
        public List<TightsType> TightsTypes { get; set; }
        public List<HighHeelImage> HighHeelImages { get; set; }
    }
}
