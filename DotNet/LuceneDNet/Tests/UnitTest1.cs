namespace Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(["https://www.uktights.com/activewear", false])]
        [InlineData(["https://www.uktights.com/activewear?currency=1", true])]
        [InlineData(["https://www.uktights.com/activewear?currency=2", true])]
        [InlineData(["https://www.uktights.com/activewear?currency=3", true])]
        [InlineData(["https://www.uktights.com/activewear?currency=4", true])]
        [InlineData(["https://www.uktights.com/activewear?currency=5", true])]
        public void UktightsTests(string uri, bool isDuplicate)
        {
            var result = LuceneDNet.Domain.WebMirror.WebScanner.IsDuplicateContent(new Uri(uri), new Uri("https://www.uktights.com"));
            Assert.Equal(isDuplicate, result);
        }
    }
}