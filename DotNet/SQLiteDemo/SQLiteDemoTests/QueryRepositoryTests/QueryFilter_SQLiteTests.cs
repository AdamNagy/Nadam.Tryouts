using Microsoft.EntityFrameworkCore;
using SQLiteDemo.DbContextService;
using SQLiteDemoTests.Models;

namespace SQLiteDemoTests.QueryRepositoryTests
{
    public class SQLiteDbFixture
    {
        public TestContext Context { get; private set; }

        public SQLiteDbFixture()
        {
            Context = CreateContext();

            if (!Context.TestModels.Any()) SeedDb();
        }

        private DbContextOptions<TestContext> GetDbOptions()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var dbPath = Path.Join(path, "query-filter-db");

            return DbContextOptionFactory<TestContext>.GetSqliteDbOption(dbPath);
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

    public class QueryFilter_SQLiteTests : QueryFilterTestDefinitions, IClassFixture<SQLiteDbFixture>
    {
        private readonly TestContext _context;

        public QueryFilter_SQLiteTests(SQLiteDbFixture fixture)
        {
            _context = fixture.Context;
        }

        public override IQueryable<SimpleModel> GetSimpleModelTable()
        {
            return _context.Set<SimpleModel>();
        }
    }
}
