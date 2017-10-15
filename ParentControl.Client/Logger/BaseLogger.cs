using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParentControl.Client.Logger.Sources;

namespace ParentControl.Client.Logger
{
    public enum MessageType
    {
        Debug,
        Info,
        Error
    }
    public abstract class BaseLogger
    {
        protected ISource Source;
        private LogType _logType;
        public BaseLogger(ISource source)
        {
            Source = source;
            //_logType = logType;
        }

        public abstract void Log(string message, MessageType type);

        public LogType LogType
        {
            get { return _logType; }
        }
    }
}
