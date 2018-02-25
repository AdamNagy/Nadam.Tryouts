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
            northwind.AddEdgeFor("Productions", "Suppliers");
            northwind.AddEdgeFor("Productions", "Customers");
            northwind.AddEdgeFor("EmpTerritories", "Employees");
            northwind.AddEdgeFor("EmpTerritories", "Territories");
            northwind.AddEdgeFor("Territories", "Regions");
            northwind.AddEdgeFor("OrderDetails", "Productions");
            northwind.AddEdgeFor("OrderDetails", "Orders");
            northwind.AddEdgeFor("Orders", "Employees");
            northwind.AddEdgeFor("Orders", "Shippers");
            northwind.AddEdgeFor("Orders", "Customers");
            northwind.AddEdgeFor("Cust", "Customers");
            northwind.AddEdgeFor("Cust", "CustomerDemographi");
        }
    }
}
