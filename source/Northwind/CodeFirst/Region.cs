using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Northwind.CodeFirst
{
    public partial class Region
    {
        [Key]
        public int RegionID { get; set; }
        public string RegionDescription { get; set; }
        public virtual ICollection<Territory> Territories { get; set; }
    }
}
