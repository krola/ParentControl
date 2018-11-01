using ParentControl.Service.Command.RefreshCommands;
using ParentControl.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Service.Command
{
    class RefreshCommand : BaseCommand
    {
        public RefreshCommand()
        {
            _subcommands = new List<ICommand>()
            {
               new RefreshTime()
            };
        }

        public override string Command => "REFRESH";

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
