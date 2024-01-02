using Moq;
using SQLiteDemo.Config;
using SQLiteDemo.DbContextService;
using SQLiteDemoTests.Models;

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

        private TestContext CreateContext()
        {
            var config = new PostgreSQLConfig("localhost:1433", "postgres", "postgres", "Minad123");

            var cloudStorage = new Mock<IStorage>();
            var localStorage = new Mock<ILocalStorage>();

            var contextFactory = new DbContextFactory<TestContext>(localStorage.Object, cloudStorage.Object);

            return contextFactory.CreateContext(config);
        }

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
