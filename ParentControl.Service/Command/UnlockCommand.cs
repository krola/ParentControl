using ParentControl.Service.Communication.Pipe;
using System;

namespace ParentControl.Service.Command
{
    class UnlockCommand : BaseCommand
    {
        public override string Command => "UNLOCK";

        public override void PrintInfo()
        {
            Console.WriteLine(Command + " - Unlock workstation");
        }

        protected override void Do(string[] args)
        {
            Context.Locked = false;
            NotificationPipe.Send("Stacja zablokowana", Infrastructure.Constants.NotificationType.Unlock);
        }
    }
}
