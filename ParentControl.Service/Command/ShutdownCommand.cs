using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Service.Command
{
    class ShutdownCommand : BaseCommand
    {
        public override string Command => "SHUTDOWN";

        public override void PrintInfo()
        {
            Console.WriteLine(Command + " - Shutdown workstation");
        }

        protected override void Do(string[] args)
        {
            #if DEBUG
                        Console.WriteLine("ShutdownCommand executed");
            #else
                        Process.Start("shutdown", "/s /t 60");
            #endif

        }
    }
}
