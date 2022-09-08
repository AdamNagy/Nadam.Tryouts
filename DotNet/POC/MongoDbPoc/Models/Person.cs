using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbPoc.Models
{
    internal class Person
    {
        public string RecId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string IdNumber { get; set; }
        public string PassportNumber { get; set; }
        public string TaxNumber { get; set; }

        public string AddressId { get; set; }
        public Address Address { get; set; }
    }
}
