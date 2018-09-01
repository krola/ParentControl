using ParentControl.Service.Command.CloseCommands;
using ParentControl.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Service.Command
{
    class CloseCommand : BaseCommand
    {
        private List<ICommand> _subcommands;

        public CloseCommand()
        {
            _subcommands = new List<ICommand>()
            {
                new CloseSession()
            };
        }

        public override string Command => "CLOSE";

        public override void PrintInfo()
        {
            Console.WriteLine("Arguments:");
            foreach(var subcommand in _subcommands)
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
