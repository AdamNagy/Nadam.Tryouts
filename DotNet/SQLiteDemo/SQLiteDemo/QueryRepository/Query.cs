namespace SQLiteDemo.QueryRepository
{
    public struct Query
    {
        public IEnumerable<string> PropertySelection { get; set; }
        public IEnumerable<FilterDefinition> Filters { get; set; }
        public IEnumerable<OrderDefinition> Orderings { get; set; }

        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public struct FilterDefinition
    {
        public string PropertyName { get; set; }
        public Object ReferenceValue { get; set; }
        public FilterComparer Operation { get; set; }
    }

    public enum FilterComparer
    {
        Equals = 0,
        GreaterThan,
        LessThan,

        Contains,
        StartsWith,
        EndsWith,
    }

    public struct OrderDefinition
    {
        public string PropertyName { get; set; }
        public OrderParam Param { get; set; }
    }

    public enum OrderParam
    {
        Asc = 0, Desc = 1,
    }
}
