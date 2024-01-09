using Microsoft.EntityFrameworkCore;
using Nadam.DataServices.QueryExpression;

namespace Nadam.DataServices.QueryRepository
{
    public class Repository<T> where T : class
    {
        private readonly IQueryable<T> _dbSet;

        public Repository(DbContext context)
        {
            _dbSet = context.Set<T>().AsNoTracking();
        }

        public async Task<QueryResult<T>> Get(Query query)
        {
            var filtered = QueryFilter.Filter(_dbSet, query.Filters);
            var numberOfItems = await GetCountFor(query.Filters);

            return new QueryResult<T>()
            {
                NumberOfItems = numberOfItems,
                Query = query,
                Result = filtered.ToList()
            };
        }

        public async Task<int> GetCountFor(IEnumerable<FilterDefinition> filters)
            => await QueryFilter.Filter(_dbSet, filters).CountAsync();

        public async Task<T> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetBulk(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }
    }
}
