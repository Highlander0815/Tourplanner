using System;
using System.IO;

namespace BLL.Logging
{
    /// <summary>
    /// Wraps the log4j2 logger instances by realizing interface ILoggerWrapper.
    /// This avoids direct dependencies to log4j2 package.
    /// </summary>
    public class Log4NetWrapper : ILoggerWrapper
    {
        private log4net.ILog _logger;

        public static Log4NetWrapper CreateLogger(string configPath, string caller)
        {
            if (!File.Exists(configPath))
            {
                throw new ArgumentException("Does not exist.", nameof(configPath));
            }

            log4net.Config.XmlConfigurator.Configure(new FileInfo(configPath));
            var logger = log4net.LogManager.GetLogger(caller);  // System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
            return new Log4NetWrapper(logger);
        }

        public Log4NetWrapper(log4net.ILog logger)
        {
            this._logger = logger;
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }
        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }
        public void Info(string message)
        {
            _logger.Info(message);
        }
    }
}
