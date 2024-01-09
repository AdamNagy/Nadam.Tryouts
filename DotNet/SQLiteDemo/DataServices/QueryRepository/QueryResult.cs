using Nadam.DataServices.QueryExpression;

namespace Nadam.DataServices.QueryRepository
{
    public class QueryResult<T>
    {
        public Query Query { get; set; }

        public int NumberOfItems { get; set; }

        public IList<T> Result { get; set; }
    }
}
