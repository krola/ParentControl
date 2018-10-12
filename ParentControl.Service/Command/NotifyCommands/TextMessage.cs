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
    class TextMessage : BaseCommand
    {
        public TextMessage()
        {
        }

        public override string Command => "TEXT";

        public override void PrintInfo()
        {
            Console.WriteLine("\t" + Command + " [text]- Show notification board with text.");
        }

        protected override void Do(string[] args)
        {
            NotificationPipe.Send(string.Join(" ", args), Infrastructure.Constants.NotificationType.Ok);
        }
    }
}
