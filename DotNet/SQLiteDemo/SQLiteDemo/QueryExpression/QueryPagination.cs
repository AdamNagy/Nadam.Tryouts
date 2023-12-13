namespace SQLiteDemo.QueryRepository
{
    public static class QueryPagination
    {
        public static IQueryable<TEntity> Paginate<TEntity>(this IQueryable<TEntity> query, int page, int pageSize)
            => query.Skip((page - 1) * pageSize).Take(pageSize);
    }
}
