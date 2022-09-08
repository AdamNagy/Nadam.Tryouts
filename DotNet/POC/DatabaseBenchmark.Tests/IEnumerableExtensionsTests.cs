using Xunit;
using DatabaseBenchmark.Extensions;

namespace DatabaseBenchmark.Tests
{
    public class IEnumerableExtensionsTests
    {
        [Fact]
        public void Should_Split_Equally()
        {
            var data = new List<int>() { 1,2,3,4,5,6,7,8,9 };
            var result = data.Split(3).ToList();

            Assert.Equal(3, result.Count());

            var expected = new List<List<int>>(3) 
            {
               new List<int>(){ 1,2,3 },
               new List<int>(){ 4,5,6 },
               new List<int>(){ 7,8,9 }
            };

            for (int i = 0; i < 3; i++)
            {
                var resultSplit = result[i].ToList();
                for (int j = 0; j < 3; j++)
                {
                    Assert.Equal(expected[i][j], resultSplit[j]);
                }
            }
        }
    }
}