using Microsoft.EntityFrameworkCore;
using SQLiteDemo;
using SQLiteDemo.Config;

namespace SQLiteDemoTests.QueryRepositoryTests
{
    public class PostgreSQLDbFixture
    {
        public TestContext Context { get; private set; }

        public PostgreSQLDbFixture()
        {
            Context = CreateContext();

            if (!Context.TestModels.Any()) SeedDb();
        }

        private DbContextOptions<TestContext> GetDbOptions()
        {
            var config = new PostgreSQLConfig("localhost:5432", "unit-test-query-db", "postgres", "postgre");

            return DbContextOptionFactory<TestContext>.GetPostgreSqlDbOptions("unit-test-query-db", config);
        }

        private TestContext CreateContext()
            => new TestContext(GetDbOptions());

        private void SeedDb()
        {
            foreach (var record in SimpleModel.GenerateData())
            {
                Context.TestModels.Add(record);
            }
            Context.SaveChanges();
        }
    }

    public class QueryFilter_PostgreSQLTests : QueryFilterTestDefinitions, IClassFixture<PostgreSQLDbFixture>
    {
        private readonly TestContext _context;

        public QueryFilter_PostgreSQLTests(PostgreSQLDbFixture fixture)
        {
            _context = fixture.Context;
        }

        public override IQueryable<SimpleModel> GetSimpleModelTable()
        {
            return _context.Set<SimpleModel>();
        }
    }
}
