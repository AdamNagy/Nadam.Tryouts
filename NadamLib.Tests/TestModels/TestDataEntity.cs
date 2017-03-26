using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NadamLib.Tests.TestModels
{
    public class TestDataEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public ColorEnum ColorE { get; set; }
        public ColorClass ColorC { get; set; }
    }
}
