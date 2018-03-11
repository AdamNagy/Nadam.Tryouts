using Nadam.Global.JsonDb.DatabaseGraph;
using System.Collections.Generic;

namespace Nadam.Lib.JsonDb.Test
{
    public class NorthwindDbGraphSeeder
    {
        public static RelationalDatabaseGraph GenerateNorthwindDbGraph()
        {
            var northwindSeeder = new NorthwindDbGraphSeeder();
            var northwind = new RelationalDatabaseGraph();
            foreach (var table in northwindSeeder.GetNorthwindTables())
            {
                northwind.AddTable(table);
            }

            northwindSeeder.SeedNorthwindTableDependencies(ref northwind);

            return northwind;
        }

        private IEnumerable<string> GetNorthwindTables()
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

        private void SeedNorthwindTableDependencies(ref RelationalDatabaseGraph northwind)
        {
            northwind.AddReferenceFor("Productions", "Suppliers");
            northwind.AddReferenceFor("Productions", "Customers");
            northwind.AddReferenceFor("EmpTerritories", "Employees");
            northwind.AddReferenceFor("EmpTerritories", "Territories");
            northwind.AddReferenceFor("Territories", "Regions");
            northwind.AddReferenceFor("OrderDetails", "Productions");
            northwind.AddReferenceFor("OrderDetails", "Orders");
            northwind.AddReferenceFor("Orders", "Employees");
            northwind.AddReferenceFor("Orders", "Shippers");
            northwind.AddReferenceFor("Orders", "Customers");
            northwind.AddReferenceFor("Cust", "Customers");
            northwind.AddReferenceFor("Cust", "CustomerDemographi");
        }
    }
}
