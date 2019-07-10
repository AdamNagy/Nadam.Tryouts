using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Linq;

namespace CustomQueryable.Tests
{
    [TestClass]
    public class LinqToSqlTests
    {
        [TestMethod]
        public void Test1()
        {
            using (SqlConnection con = new SqlConnection(""))
            {
                Northwind db = new Northwind(con);
                var query = db.Customers.Where(c => c.City == "London");
                Assert.AreEqual("SELECT * FROM (SELECT * FROM Customers) AS T WHERE (City = 'London')", query.ToString());
            }
        }
    }
}
