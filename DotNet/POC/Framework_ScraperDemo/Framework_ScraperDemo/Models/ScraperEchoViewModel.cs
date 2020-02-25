namespace Framework_ScraperDemo.Models
{
    public class ScraperEchoViewModel
    {
        public string Url;
        public string HtmlString { get; private set; }

        public ScraperEchoViewModel(string htmlString)
        {
            HtmlString = htmlString;
        }
    }
}