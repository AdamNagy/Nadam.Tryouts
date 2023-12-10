using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteDemo.QueryRepository
{
    public class QueryRepository<T> where T : class
    {
        private readonly IQueryable<T> _dbSet;

        public QueryRepository(DbContext context)
        {
            _dbSet = context.Set<T>().AsNoTracking();
        }

        public async Task<QueryResult<T>> Get(Query query)
        {
            throw new NotImplementedException();
        }

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
