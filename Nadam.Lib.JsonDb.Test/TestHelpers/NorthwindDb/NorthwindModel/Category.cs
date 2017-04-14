using System.Collections.Generic;

namespace Nadam.Lib.JsonDb.Test.NorthwindModel
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
