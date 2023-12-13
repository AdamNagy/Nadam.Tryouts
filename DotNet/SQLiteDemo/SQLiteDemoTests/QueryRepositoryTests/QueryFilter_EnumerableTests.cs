using SQLiteDemo.QueryRepository;
using SQLiteDemoTests.Models;

namespace SQLiteDemoTests.QueryRepositoryTests
{
    public class QueryFilter_EnumerableTests : QueryFilterTestDefinitions
    {
        public override IQueryable<SimpleModel> GetSimpleModelTable()
            => SimpleModel.GenerateData().AsQueryable();
    }
}
