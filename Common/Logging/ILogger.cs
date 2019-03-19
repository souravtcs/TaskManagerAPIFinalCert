using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging
{
    public interface ILogger
    {
        void Error(string msg);
        void Debug(string msg);
        void Warning(string msg);
        void Info(string msg);
    }
}
