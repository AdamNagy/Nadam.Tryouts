using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nadam.Lib.JsonDb;
using Nadam.Lib.JsonDb.Test.NorthwindModel;

namespace Nadam.Lib.JsonDb.Test.TestHelpers
{
    public class NorthwindJsonContext : JsonDbEngineContext
    {
        public NorthwindJsonContext(string path) : base("path=../../app_data/NorthwindJson") {}

        public virtual List<Category> Categories { get; set; }
        public virtual List<CustomerDemographic> CustomerDemographics { get; set; }
        public virtual List<Customer> Customers { get; set; }
        public virtual List<Employee> Employees { get; set; }
        public virtual List<Order_Detail> Order_Details { get; set; }
        public virtual List<Order> Orders { get; set; }
        public virtual List<Product> Products { get; set; }
        public virtual List<Region> Regions { get; set; }
        public virtual List<Shipper> Shippers { get; set; }
        public virtual List<Supplier> Suppliers { get; set; }
        public virtual List<Territory> Territories { get; set; }
    }
}
