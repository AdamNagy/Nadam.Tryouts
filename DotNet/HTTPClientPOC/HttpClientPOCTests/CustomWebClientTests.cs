using HTTPClientPOC;

namespace WebClientPOCTests;

public class CustomWebClientTests
{
    [Fact]
    public async Task Test1()
    {
        var sut = new CustomWebClient();
        var result = await sut.Requeset("https://yummies.urlgalleries.net/porn-gallery-6449675/al-5497-kristina?a=1000");

        Assert.Equal(83, result.Where(p => p.EndsWith("jpg")).Count());
    }
}
