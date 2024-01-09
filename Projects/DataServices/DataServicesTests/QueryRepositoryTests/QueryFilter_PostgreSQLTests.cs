using Moq;
using Nadam.DataServices.Config;
using Nadam.DataServices.Tests.EntityModels;

namespace Nadam.DataServices.Tests.QueryRepositoryTests
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
            foreach (var record in FilteringModel.GenerateData())
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
