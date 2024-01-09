using Nadam.DataServices.QueryExpression;
using Nadam.DataServices.Tests.EntityModels;

namespace Nadam.DataServices.Tests.QueryRepositoryTests
{
    public abstract class QueryFilterTestDefinitions
    {
        public abstract IQueryable<FilteringModel> GetFilteringModelTable();
        public abstract IQueryable<OrderingModel> GetOrderingModelTable();

        #region Filtering
        [Fact]
        public void GreaterThan_Int_Some()
        {
            // Arrange
            var inMemoryData = GetFilteringModelTable();
            var s = inMemoryData.Select(p => p.DoubleProp).ToList();

            var filter = new FilterDefinition()
            {
                Operation = FilterComparer.GreaterThan,
                PropertyName = "GreaterThanSome",
                ReferenceValue = 50
            };

            // Act
            var resultQuery = QueryFilter.Filter(inMemoryData, filter);
            var result = resultQuery.ToList();

            // Assert
            Assert.Equal(49, result.Count);
            Assert.Equal(51, result.Min().GreaterThanSome);
        }

        [Fact]
        public void GreaterThan_Int_All()
        {
            // Arrange
            var inMemoryData = GetFilteringModelTable();

            var filter = new FilterDefinition()
            {
                Operation = FilterComparer.GreaterThan,
                PropertyName = "GreaterThanAll",
                ReferenceValue = 50
            };

            // Act
            var resultQuery = QueryFilter.Filter(inMemoryData, filter);
            var result = resultQuery.ToList();

            // Assert
            Assert.Equal(100, result.Count);
            Assert.Equal(51, result.Min().GreaterThanAll);
        }

        [Fact]
        public void GreaterThan_DateTime()
        {
            // Arrange
            var inMemoryData = GetFilteringModelTable();

            var referenceVal = DateTime.Now.AddDays(-500);
            var filter = new FilterDefinition()
            {
                Operation = FilterComparer.GreaterThan,
                PropertyName = "DateTimeProp",
                ReferenceValue = referenceVal
            };

            // Act
            var resultQuery = QueryFilter.Filter(inMemoryData, filter);
            var result = resultQuery.ToList();

            // Assert
            Assert.Equal(50, result.Count);
        }

        [Fact]
        public void GreaterThan_Double()
        {
            // Arrange
            var inMemoryData = GetFilteringModelTable();

            var referenceVal = 4.9;
            var filter = new FilterDefinition()
            {
                Operation = FilterComparer.GreaterThan,
                PropertyName = "DoubleProp",
                ReferenceValue = referenceVal
            };

            // Act
            var resultQuery = QueryFilter.Filter(inMemoryData, filter);
            var result = resultQuery.ToList();

            // Assert
            Assert.Equal(50, result.Count);
        }

        [Fact]
        public void StartsWith_Some()
        {
            // Arrange
            var inMemoryData = GetFilteringModelTable();

            var referenceVal = "4";
            var filter = new FilterDefinition()
            {
                Operation = FilterComparer.StartsWith,
                PropertyName = "StartsWithSomeProp",
                ReferenceValue = referenceVal
            };

            // Act
            var resultQuery = QueryFilter.Filter(inMemoryData, filter);
            var result = resultQuery.ToList();

            // Assert
            Assert.Equal(20, result.Count);
        }

        [Fact]
        public void StartsWith_All()
        {
            // Arrange
            var inMemoryData = GetFilteringModelTable();

            var referenceVal = "4";
            var filter = new FilterDefinition()
            {
                Operation = FilterComparer.StartsWith,
                PropertyName = "StartsWithAllProp",
                ReferenceValue = referenceVal
            };

            // Act
            var resultQuery = QueryFilter.Filter(inMemoryData, filter);
            var result = resultQuery.ToList();

            // Assert
            Assert.Equal(100, result.Count);
        }

        [Fact]
        public void Contains_All()
        {
            // Arrange
            var inMemoryData = GetFilteringModelTable();

            var referenceVal = "4";
            var filter = new FilterDefinition()
            {
                Operation = FilterComparer.Contains,
                PropertyName = "ContainsAllProp",
                ReferenceValue = referenceVal
            };

            // Act
            var resultQuery = QueryFilter.Filter(inMemoryData, filter);
            var result = resultQuery.ToList();

            // Assert
            Assert.Equal(100, result.Count);
        }

        [Fact]
        public void Contains_Some()
        {
            // Arrange
            var inMemoryData = GetFilteringModelTable();

            var referenceVal = "4";
            var filter = new FilterDefinition()
            {
                Operation = FilterComparer.Contains,
                PropertyName = "ContainsSomeProp",
                ReferenceValue = referenceVal
            };

            // Act
            var resultQuery = QueryFilter.Filter(inMemoryData, filter);
            var result = resultQuery.ToList();

            // Assert
            Assert.Equal(20, result.Count);
        }

        [Fact]
        public void EndsWith_All()
        {
            // Arrange
            var inMemoryData = GetFilteringModelTable();

            var referenceVal = "4";
            var filter = new FilterDefinition()
            {
                Operation = FilterComparer.EndsWith,
                PropertyName = "EndsWithAllProp",
                ReferenceValue = referenceVal
            };

            // Act
            var resultQuery = QueryFilter.Filter(inMemoryData, filter);
            var result = resultQuery.ToList();

            // Assert
            Assert.Equal(100, result.Count);
        }

        [Fact]
        public void EndsWith_Some()
        {
            // Arrange
            var inMemoryData = GetFilteringModelTable();

            var referenceVal = "4";
            var filter = new FilterDefinition()
            {
                Operation = FilterComparer.EndsWith,
                PropertyName = "EndsWithSomeProp",
                ReferenceValue = referenceVal
            };

            // Act
            var resultQuery = QueryFilter.Filter(inMemoryData, filter);
            var result = resultQuery.ToList();

            // Assert
            Assert.Equal(20, result.Count);
        }
        #endregion

        #region Order
        #endregion

        #region Joins
        #endregion

        #region Projection
        #endregion
    }
}
