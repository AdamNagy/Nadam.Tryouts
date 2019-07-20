using System;

namespace InfrastructureDemo.Logging
{
    /// <summary>
    /// This is a concrete logger API adapter named Log4Net
    /// As I don't know this I will use NLog instead
    /// </summary>
    public class Log4NetAdapter : ILogger
    {
        //private readonly log4net.ILog _log;

        //public Log4NetAdapter()
        //{
        //    XmlConfigurator.Configure();
        //    _log = LogManager
        //     .GetLogger(ApplicationSettingsFactory.GetApplicationSettings().LoggerName);
        //}

        //public void Log(string message)
        //{
        //    _log.Info(message);
        //}
        public void Log(string message)
        {
            throw new NotImplementedException("This is a concrete logger API adapter named Log4Net. Refer to class documentation for details");
        }
    }

}
