using Moq;
using Nadam.DataServices.Config;
using Nadam.DataServices.Tests.EntityModels;

namespace Nadam.DataServices.Tests.QueryRepositoryTests
{
    public class SQLiteDbFixture
    {
        public TestContext Context { get; private set; }

        public SQLiteDbFixture()
        {
            Context = CreateContext();

            if (!Context.TestModels.Any()) SeedDb();
        }

        private TestContext CreateContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var dbPath = Path.Join(path, "query-filter-db");

            var cloudStorage = new Mock<IStorage>();
            var localStorage = new Mock<ILocalStorage>();

            var contextFactory = new DbContextFactory<TestContext>(localStorage.Object, cloudStorage.Object);

            var options = new SqliteConfig()
            {
                DbFileName = dbPath,
            };

            return contextFactory.CreateContext(options);
        }

        private void SeedDb()
        {
            foreach (var record in FilteringModel.GenerateData())
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

        public override IQueryable<FilteringModel> GetFilteringModelTable()
        {
            return _context.Set<FilteringModel>();
        }

        public override IQueryable<OrderingModel> GetOrderingModelTable()
        {
            throw new NotImplementedException();
        }
    }
}
