using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Northwind.CodeFirst
{
    public partial class Territory
    {
        [Key]
        public string TerritoryID { get; set; }
        public string TerritoryDescription { get; set; }
        public int RegionID { get; set; }
    
        public virtual Region Region { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
