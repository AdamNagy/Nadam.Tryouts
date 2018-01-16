using System.Collections.Generic;

namespace Nadam.Lib.JsonDb.Test.NorthwindModel
{
    public class Region
    {
        public int RegionID { get; set; }
        public string RegionDescription { get; set; }
        public virtual ICollection<Territory> Territories { get; set; }
    }
}
