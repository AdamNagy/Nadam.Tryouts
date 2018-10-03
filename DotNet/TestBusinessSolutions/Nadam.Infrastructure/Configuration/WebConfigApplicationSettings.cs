using System.Configuration;

namespace Nadam.Infrastructure.Configuration
{
    public class WebConfigApplicationSettings : IApplicationSettings
    {
        /// <summary>
        /// Need to add new section to Web app's web.config
        /// <appSettings>
        ///     <add key ="LoggerName" value="AgathaLogger"/>
        /// </appSettings >
        /// </summary>
        public string LoggerName => ConfigurationManager.AppSettings["LoggerName"];

        public string NumberOfResultsPerPage => ConfigurationManager.AppSettings["NumberOfResultsPerPage"];

        public string JanrainApiKey => ConfigurationManager.AppSettings["JanrainApiKey"];

        public string PayPalBusinessEmail => ConfigurationManager.AppSettings["PayPalBusinessEmail"];

        public string PayPalPaymentPostToUrl => ConfigurationManager.AppSettings["PayPalPaymentPostToUrl"];
    }

}
