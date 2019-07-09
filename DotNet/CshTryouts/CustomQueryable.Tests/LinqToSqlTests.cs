using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;
using System.Linq;

namespace CustomQueryable.Tests
{
    [TestClass]
    public class LinqToSqlTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (SqlConnection con = new SqlConnection("Server=NADAM-DESKTOP\\SQLEXPRESS;Database=MIV;Trusted_Connection=True;"))
            {
                Northwind db = new Northwind(con);
                var query = db.Customers.Where(c => c.City == "London");
                Assert.AreEqual("SELECT * FROM (SELECT * FROM Customers) AS T WHERE (City = 'London')", query.ToString());
            }
        }
    }
}
