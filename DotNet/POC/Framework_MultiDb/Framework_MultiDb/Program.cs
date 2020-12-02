using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiDb
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "server=mysql.caesar.elte.hu;database=qweasd;uid=qweasd;password=hPHyrSLuYFSVmH0Q";
            using (var connection = new MySqlConnection(connectionString))
            {
                // Create database if not exists
                using (var contextDB = new YmPronoContext(connection, false))
                {
                    contextDB.Database.CreateIfNotExists();
                }
            }
        }
    }
}
