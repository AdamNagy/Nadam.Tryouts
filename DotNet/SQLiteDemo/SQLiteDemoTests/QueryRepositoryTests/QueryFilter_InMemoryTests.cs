using Microsoft.EntityFrameworkCore;
using SQLiteDemo;

namespace SQLiteDemoTests.QueryRepositoryTests
{
    public class InMemoryDbFixture
    {
        public TestContext Context { get; private set; }

        public InMemoryDbFixture()
        {
            Context = CreateContext();
            SeedDb();
        }

        private DbContextOptions<TestContext> GetDbOptions()
            => DbContextOptionFactory<TestContext>.GetInMemoryDbOptions("in-memory-ef");

        private TestContext CreateContext()
            => new TestContext(GetDbOptions());

        private void SeedDb()
        {
            foreach (var record in TestModel.GenerateData())
            {
                Context.TestModels.Add(record);
            }
            Context.SaveChanges();
        }
    }

    public class QueryFilter_InMemoryTests : QueryFilterTestDefinitions, IClassFixture<InMemoryDbFixture>
    {
        private readonly TestContext _context;

        public QueryFilter_InMemoryTests(InMemoryDbFixture fixture)
        {
            _context = fixture.Context;
        }

        public override IQueryable<TestModel> GetTable()
        {
            return _context.Set<TestModel>();
        }
    }
}
