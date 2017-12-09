using System.Collections.Generic;

namespace Nadam.Lib.JsonDb.Test.NorthwindModel
{
    public class Shipper
    {
        public int ShipperID { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }       
        public virtual ICollection<Order> Orders { get; set; }
    }
}
