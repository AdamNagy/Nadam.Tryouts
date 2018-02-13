using System;
using System.Collections.Generic;
using Nadam.Global.JsonDb.DatabaseGraph;

namespace Nadam.ConsoleTest
{
    class GraphTestConsole
    {
        public static void TestRunner()
        {
            var runner = new GraphTestConsole();
            runner.Run();
        }

        private void Run()
        {
            //var graph = new DbModelGraph();
            //graph.AddTable("BaseTable", SeedTables());
            //graph.AddTable("Table_B", SeedTables2());
            //var dependecies = graph.GetDependentTables("BaseTable").ToList();
            //var dep = graph.GetDependentTables("Table_B").ToList();
            //graph.AddTable("Table_B");

            //var it = graph.DependecyIterator();
            //it.Reset();
            //while(it.MoveNext())
            //{
            //    Console.WriteLine(it.Current.Value);
            //}

            //var northwind = new DbModelGraph();
            //northwind.AddTables(SeedNorthwindTables());

            //// base table connections
            //northwind.AddDependecy("Productions", "Suppliers");
            //northwind.AddDependecy("Productions", "Customers");
            //northwind.AddDependecy("EmpTerritories", "Employees");
            //northwind.AddDependecy("EmpTerritories", "Territories");
            //northwind.AddDependecy("Territories", "Regions");
            //northwind.AddDependecy("OrderDetails", "Productions");
            //northwind.AddDependecy("OrderDetails", "Orders");
            //northwind.AddDependecy("Orders", "Employees");
            //northwind.AddDependecy("Orders", "Shippers");
            //northwind.AddDependecy("Orders", "Customers");
            //northwind.AddDependecy("Cust", "Customers");
            //northwind.AddDependecy("Cust", "CustomerDemographi");

            //// additional connection for testing
            //northwind.AddDependecy("Cust", "Orders");
            //northwind.AddDependecy("Cust", "EmpTerritories");

            //foreach (TableNode table in northwind)
            //{
            //    Console.WriteLine("\tTable: " + table.TableName);
            //    foreach (var item in northwind.GetDependentTables(table.Value))
            //    {
            //        Console.WriteLine(item);
            //    }
            //    Console.WriteLine("---------------------");
            //}
            Console.WriteLine("\n**************************");
            //var it = northwind.DependecyIterator();
            //it.Reset();
            //while (it.MoveNext())
            //{
            //    Console.WriteLine(it.Current.Value);
            //}
            
            //foreach (var it in northwind.DependecyIteration())
            //{
            //    Console.WriteLine(it.Value);
            //}
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

        private IEnumerable<string> SeedTables()
        {
            return new List<string>()
            {
                "Table_A",
                "Table_B",
                "Table_C"
            };
        }

        private IEnumerable<string> SeedTables2()
        {
            return new List<string>()
            {
                "Table_D",
                "Table_E"
            };
        }
    }
}
