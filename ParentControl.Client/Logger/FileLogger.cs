using System;
using ParentControl.Client.Logger.Sources;

namespace ParentControl.Client.Logger
{
    public class FileLogger : BaseLogger
    {
        public static LogType Type => LogType.File;
        public override void Log(string message, MessageType type)
        {
            Source.Write($"[{type.ToString()}][{DateTime.Now}]: {message}");
        }

        public FileLogger() : base(new FileSource())
        {
          
        }
    }
}
