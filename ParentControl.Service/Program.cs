using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ParentControl.Service.Command;

namespace ParentControl.Service
{
    class Program
    {
        [DllImport("Kernel32")]
        public static extern bool SetConsoleCtrlHandler(HandlerRoutine Handler, bool Add);

        public delegate bool HandlerRoutine(CtrlTypes CtrlType);

        public enum CtrlTypes
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }

        const string EXIT_COMMAND = "EXIT";

        private static CommandExecuter commandExecuter;

        static void Main(string[] args)
        {
            var core = new Core();
            commandExecuter = new CommandExecuter();
            HandlerRoutine hr = new HandlerRoutine(ConsoleCtrlCheck);
            GC.KeepAlive(hr);
            SetConsoleCtrlHandler(hr, true);

            core.Init();
            core.Run();

            var command = string.Empty;
            while(!command.Equals(EXIT_COMMAND, StringComparison.InvariantCultureIgnoreCase) && command != null)
            {
                Console.Write("> ");
                command = Console.ReadLine();
                commandExecuter.Execute(command);
            }

            commandExecuter.Execute("CLOSE SESSION");
        }

        private static bool ConsoleCtrlCheck(CtrlTypes ctrlType)
        {
            switch (ctrlType)
            {
                case CtrlTypes.CTRL_C_EVENT:
                case CtrlTypes.CTRL_BREAK_EVENT:                    
                case CtrlTypes.CTRL_CLOSE_EVENT:
                case CtrlTypes.CTRL_LOGOFF_EVENT:
                case CtrlTypes.CTRL_SHUTDOWN_EVENT:
                    commandExecuter.Execute("CLOSE SESSION");
                        break;

            }
            return true;
        }
    }
}
