using Nadam.DataServices.Tests.EntityModels;

namespace Nadam.DataServices.Tests.QueryRepositoryTests
{
    public class QueryFilter_EnumerableTests : QueryFilterTestDefinitions
    {
        public override IQueryable<FilteringModel> GetFilteringModelTable()
            => FilteringModel.GenerateData().AsQueryable();

        public override IQueryable<OrderingModel> GetOrderingModelTable()
        {
            throw new NotImplementedException();
        }
    }
}
