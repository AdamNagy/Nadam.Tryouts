using SQLiteDemo.QueryRepository;

namespace SQLiteDemoTests.QueryRepositoryTests
{
    public class QueryFilter_EnumerableTests : QueryFilterTestDefinitions
    {
        public override IQueryable<TestModel> GetTable()
            => TestModel.GenerateData().AsQueryable();
    }
}
