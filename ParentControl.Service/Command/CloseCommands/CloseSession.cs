﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Service.Command.CloseCommands
{
    class CloseSession : BaseCommand
    {
        public override string Command => "SESSION";

        public override void PrintInfo()
        {
            Console.WriteLine("\t" + Command + " - Close session when in online mode");
        }

        protected override void Do(string[] args)
        {
            Context.JobManager.Stop();
            Context.ParentControlService.SessionService.EndSession(Context.ActiveSession);
        }
    }
}
