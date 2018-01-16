using System.Collections.Generic;

namespace Nadam.ConsoleTest.MIV
{
    public class HighHeel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IEnumerable<HighHeelImage> Images { get; set; }
    }
}
