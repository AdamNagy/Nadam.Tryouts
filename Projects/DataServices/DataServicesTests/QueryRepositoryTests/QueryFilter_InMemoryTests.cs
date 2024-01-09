using Moq;
using Nadam.DataServices.Config;
using Nadam.DataServices.Tests.EntityModels;

namespace Nadam.DataServices.Tests.QueryRepositoryTests
{
    public class InMemoryDbFixture
    {
        public TestContext Context { get; private set; }

        public InMemoryDbFixture()
        {
            Context = CreateContext();
            SeedDb();
        }

        private TestContext CreateContext()
        {
            var config = new InMemoryEfDbConfig("query-filter-db");

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

    public class QueryFilter_InMemoryTests : QueryFilterTestDefinitions, IClassFixture<InMemoryDbFixture>
    {
        private readonly TestContext _context;

        public QueryFilter_InMemoryTests(InMemoryDbFixture fixture)
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
