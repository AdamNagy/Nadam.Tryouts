using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Northwind.CodeFirst
{    
    public partial class CustomerDemographic
    {
        [Key]
        public string CustomerTypeID { get; set; }
        public string CustomerDesc { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
