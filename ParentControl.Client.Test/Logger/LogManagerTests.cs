using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using ParentControl.Client.Logger;

namespace ParentControl.Client.Test.Logger
{
    [TestFixture]
    public class LogManagerTests
    {
        [Test]
        public void LoadLoggers()
        {
            var logger = new LogManager(LogType.File);
        }

        [Test]
        public void WriteToFileLog()
        {
            var logger = new LogManager(LogType.File);
            logger.Log.Log("test", MessageType.Info);
        }
    }
}
