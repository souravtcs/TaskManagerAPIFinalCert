using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Common.Logging
{    
    public class Logger:ILogger
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //LogManager.GetLogger(typeof(Logger));
        public void Error(string msg)
        {
            log.Error(msg);
        }
        public void Debug(string msg)
        {
            log.Debug(msg);
        }
        public void Warning(string msg)
        {
            log.Warn(msg);
        }
        public void Info(string msg)
        {
            log.Info(msg);
        }
    }
}
