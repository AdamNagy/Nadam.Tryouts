using SQLiteDemo.QueryRepository;

namespace SQLiteDemoTests
{
    public class QueryFilter_InMemoryTests
    {
        [Fact]
        public void FilteringNumberList()
        {
            // Arrange
            var inMemoryData = Enumerable.Range(0, 100).Select(p => new TestModel()
            {
                IntProp = p
            }).AsQueryable();

            var filter = new FilterDefinition()
            {
                Operation = FilterComparer.GreaterThan,
                PropertyName = "IntProp",
                ReferenceValue = 50
            };

            // Act
            var resultQuery = QueryFilter.Filter(inMemoryData, filter);
            var result = resultQuery.ToList();

            // Assert
            Assert.Equal(49, result.Count);
            Assert.Equal(51, result.Min().IntProp);
        }

        [Fact]
        public void FilteringDateList()
        {
            // Arrange
            var inMemoryData = Enumerable.Range(0, 100).Select(p => new TestModel()
            {
                IntProp = p,
                DateTimeProp = DateTime.Now.AddDays(p * 10 * -1)
            }).AsQueryable();

            var referenceVal = DateTime.Now.AddDays(-500);
            var filter = new FilterDefinition()
            {
                Operation = FilterComparer.LessThan,
                PropertyName = "DateProp",
                ReferenceValue = referenceVal
            };

            // Act
            var resultQuery = QueryFilter.Filter(inMemoryData, filter);
            var result = resultQuery.ToList();

            // Assert
            Assert.Equal(49, result.Count);
            Assert.Equal(51, result.Min().IntProp);
        }
    }
}
