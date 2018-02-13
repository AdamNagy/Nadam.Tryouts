using System.Collections.Generic;
<<<<<<< HEAD
using Nadam.Global.JsonDb;
=======
using Nadam.JsonDb;
>>>>>>> master
using Nadam.Lib.JsonDb.Test.NorthwindModel;

namespace Nadam.Lib.JsonDb.Test.TestHelpers
{
    public class NorthwindJsonContext : JsonDbEngineContext
    {
        public NorthwindJsonContext(string path, bool inmemory = true) : base(path, inmemory) {}

        public List<Category> Categories { get; set; }
        public List<CustomerDemographic> CustomerDemographics { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Order_Detail> Order_Details { get; set; }
        public List<Order> Orders { get; set; }
        public List<Product> Products { get; set; }
        public List<Region> Regions { get; set; }
        public List<Shipper> Shippers { get; set; }
        public List<Supplier> Suppliers { get; set; }
        public List<Territory> Territories { get; set; }
    }
}
