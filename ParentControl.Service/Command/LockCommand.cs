using ParentControl.Service.Communication.Pipe;
using System;

namespace ParentControl.Service.Command
{
    class LockCommand : BaseCommand
    {
        public override string Command => "LOCK";

        public override void PrintInfo()
        {
            Console.WriteLine(Command + " - Lock workstation");
        }

        protected override void Do(string[] args)
        {
            Context.Locked = true;
            NotificationPipe.Send("Stacja zablokowana", Infrastructure.Constants.NotificationType.Lock);
        }
    }
}
