using ParentControl.Service.Command.NotifyCommands;
using ParentControl.Service.Contracts;
using System;
using System.Collections.Generic;

namespace ParentControl.Service.Command
{
    class NotifyCommand : BaseCommand
    {
        public NotifyCommand()
        {
            _subcommands = new List<ICommand>()
            {
               new CloseAction(),
               new TextMessage()
            };
        }

        public override string Command => "NOTIFY";

        public override void PrintInfo()
        {
            Console.WriteLine("Arguments:");
            foreach (var subcommand in _subcommands)
            {
                subcommand.PrintInfo();
            }
        }

        protected override void Do(string[] args)
        {
            PrintInfo();
        }
    }
}
