using Nadam.Lib.DatabaseGraphs;
using System.Collections.Generic;

namespace Nadam.Lib.JsonDb.Test
{
    public class NorthwindDbGraphSeeder
    {
        public static DatabaseGraph GenerateNorthwindDbGraph()
        {
            var northwindSeeder = new NorthwindDbGraphSeeder();
            var northwind = new DatabaseGraph();
            northwind.AddTables(northwindSeeder.SeedNorthwindTables());
            northwindSeeder.SeedNorthwindTableDependencies(ref northwind);

            return northwind;
        }

        private IEnumerable<string> SeedNorthwindTables()
        {
            return new List<string>()
            {
                "Suppliers",
                "EmpTerritories",
                "Territories",
                "Regions",
                "Productions",
                "Employees",
                "OrderDetails",
                "Orders",
                "Shippers",
                "Categories",
                "Customers",
                "Cust",
                "CustomerDemographi"
            };
        }

        private void SeedNorthwindTableDependencies(ref DatabaseGraph northwind)
        {
            northwind.AddDependecy("Productions", "Suppliers");
            northwind.AddDependecy("Productions", "Customers");
            northwind.AddDependecy("EmpTerritories", "Employees");
            northwind.AddDependecy("EmpTerritories", "Territories");
            northwind.AddDependecy("Territories", "Regions");
            northwind.AddDependecy("OrderDetails", "Productions");
            northwind.AddDependecy("OrderDetails", "Orders");
            northwind.AddDependecy("Orders", "Employees");
            northwind.AddDependecy("Orders", "Shippers");
            northwind.AddDependecy("Orders", "Customers");
            northwind.AddDependecy("Cust", "Customers");
            northwind.AddDependecy("Cust", "CustomerDemographi");
        }
    }
}
