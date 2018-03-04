//using System;
//using System.Collections.Generic;
//using Nadam.Global.JsonDb.DatabaseGraph;

//namespace Nadam.ConsoleTest
//{
//    class GraphTestConsole
//    {
//        public static void TestRunner()
//        {
//            var runner = new GraphTestConsole();
//            runner.Run();
//        }

//        private void Run()
//        {
//            Console.WriteLine();
//            var northwind = new RelationalDatabaseGraph();
//            foreach (var table in GetNorthwindTables())
//            {
//                northwind.AddTable(table);
//            }

//            SeedNorthwindTableDependencies(ref northwind);

//            foreach (var table in northwind)
//            {
//                Console.WriteLine(table);
//            }
//        }

//        private IEnumerable<string> GetNorthwindTables()
//        {
//            return new List<string>()
//            {
//                "Suppliers",
//                "EmpTerritories",
//                "Territories",
//                "Regions",
//                "Productions",
//                "Employees",
//                "OrderDetails",
//                "Orders",
//                "Shippers",
//                "Categories",
//                "Customers",
//                "Cust",
//                "CustomerDemographi"
//            };
//        }

//        private void SeedNorthwindTableDependencies(ref RelationalDatabaseGraph northwind)
//        {
//            northwind.AddEdgeFor("Productions", "Suppliers");
//            northwind.AddEdgeFor("Productions", "Customers");
//            northwind.AddEdgeFor("EmpTerritories", "Employees");
//            northwind.AddEdgeFor("EmpTerritories", "Territories");
//            northwind.AddEdgeFor("Territories", "Regions");
//            northwind.AddEdgeFor("OrderDetails", "Productions");
//            northwind.AddEdgeFor("OrderDetails", "Orders");
//            northwind.AddEdgeFor("Orders", "Employees");
//            northwind.AddEdgeFor("Orders", "Shippers");
//            northwind.AddEdgeFor("Orders", "Customers");
//            northwind.AddEdgeFor("Cust", "Customers");
//            northwind.AddEdgeFor("Cust", "CustomerDemographi");
//        }
//    }
//}
