using WebClientPOC;

namespace WebClientPOCTests;

public class PlaywrightWebClientTests
{
    [Fact]
    public async Task Test1()
    {
        var sut = new PlaywrightWebClient();
        var result = await sut.Requeset("https://yummies.urlgalleries.net/porn-gallery-6449675/al-5497-kristina?a=1000");

        Assert.Equal(83, result.Where(p => p.EndsWith("jpg")).Count());
    }
}
