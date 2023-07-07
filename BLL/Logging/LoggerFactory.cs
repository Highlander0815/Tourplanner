﻿using System.Diagnostics;
using System.IO;

namespace BLL.Logging
{
    public static class LoggerFactory
    {
        public static ILoggerWrapper GetLogger()
        {
            StackTrace stackTrace = new StackTrace(1, false); //Captures 1 frame, false for not collecting information about the file
            var type = stackTrace.GetFrame(1).GetMethod().DeclaringType;
            return Log4NetWrapper.CreateLogger("./log4net.config", type.FullName);
        }
    }
}