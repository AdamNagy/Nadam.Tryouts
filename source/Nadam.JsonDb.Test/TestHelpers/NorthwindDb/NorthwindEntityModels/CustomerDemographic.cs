using System.Collections.Generic;

namespace Nadam.Lib.JsonDb.Test.NorthwindModel
{
    public class CustomerDemographic
    {
        public string CustomerTypeID { get; set; }
        public string CustomerDesc { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
