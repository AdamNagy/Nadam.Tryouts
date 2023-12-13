using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDemo.QueryRepository
{
    public class QueryResult<T>
    {
        public Query Query { get; set; }

        public int NumberOfItems { get; set; }

        public IList<T> Result { get; set; }
    }
}
