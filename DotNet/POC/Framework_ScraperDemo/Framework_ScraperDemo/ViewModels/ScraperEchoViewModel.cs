namespace Framework_ScraperDemo.Models
{
    public class ScraperEchoViewModel
    {
        public string Schema;
        public string Domain;
        public string Path;
        public string HtmlString;

        public ScraperEchoViewModel()
        {
            Schema = "";
            Domain = "";
            Path = "";
            HtmlString = "";
        }

        public ScraperEchoViewModel(string htmlString)
        {
            HtmlString = htmlString;
        }
    }
}