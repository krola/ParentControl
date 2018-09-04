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
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

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

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

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
            while(!command.Equals(EXIT_COMMAND, StringComparison.InvariantCultureIgnoreCase))
            {
                Console.Write("> ");
                command = Console.ReadLine();
                commandExecuter.Execute(command);
            }

            commandExecuter.Execute("CLOSE SESSION");
            //var handle = GetConsoleWindow();

            // Hide
            //ShowWindow(handle, SW_HIDE);
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
