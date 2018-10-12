using Newtonsoft.Json;
using ParentControl.Infrastructure.Communication.NamedPipes;
using ParentControl.Service.Communication.Pipe;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParentControl.Service.Command.NotifyCommands
{
    class CloseAction : BaseCommand
    {
        public CloseAction()
        {
        }

        public override string Command => "CLOSE";

        public override void PrintInfo()
        {
            Console.WriteLine("\t" + Command + " - Show close system notification board");
        }

        protected override void Do(string[] args)
        {
            NotificationPipe.Send("Koniec czasu!", Infrastructure.Constants.NotificationType.Shutdown);
        }
    }
}
