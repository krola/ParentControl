using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Client.Logger
{
    public enum LogType
    {
        File
    }
    public class LogManager
    {
        private BaseLogger _log;

        public LogManager(LogType logType)
        {
            var type = Assembly.GetAssembly(typeof(BaseLogger))
                .GetTypes().FirstOrDefault(myType => myType.IsClass &&
                                                     !myType.IsAbstract &&
                                                     myType.IsSubclassOf(typeof(BaseLogger)) &&
                                                     (LogType) myType.GetProperty("Type").GetValue(null, null) ==
                                                     logType);

            if (type != null)
            {
                _log = (BaseLogger) Activator.CreateInstance(type);
            }
        }

        public BaseLogger Log
        {
            get { return _log; }
        }
    }
}
