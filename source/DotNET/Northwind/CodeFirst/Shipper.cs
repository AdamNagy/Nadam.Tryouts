using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Northwind.CodeFirst
{    
    public partial class Shipper
    {
        [Key]
        public int ShipperID { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
