using ILogger;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger1
{
    

    public class Log : Logger, ILog
    {
        
        void ILog.Debug(string format, params object[] args)
        {
            Debug(format);
        }

        void ILog.Error(string format, params object[] args)
        {
            Error(format);
        }

        void ILog.Fatal(string format, params object[] args)
        {
            Fatal(format);
        }

        void ILog.Info(string format, params object[] args)
        {
            Info(format);
        }

        void ILog.Trace(string format, params object[] args)
        {
            Trace(format);
        }

        void ILog.Warn(string format, params object[] args)
        {
            Warn(format);
        }

        bool ILog.IsDebugEnabled
        {
            get { throw new NotImplementedException(); }
        }

        bool ILog.IsErrorEnabled
        {
            get { throw new NotImplementedException(); }
        }

        bool ILog.IsFatalEnabled
        {
            get { throw new NotImplementedException(); }
        }

        bool ILog.IsInfoEnabled
        {
            get { throw new NotImplementedException(); }
        }

        bool ILog.IsTraceEnabled
        {
            get { throw new NotImplementedException(); }
        }

        bool ILog.IsWarnEnabled
        {
            get { throw new NotImplementedException(); }
        }
    }
}
