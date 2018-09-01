using ParentControl.Service.Command.JobCommands;
using ParentControl.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParentControl.Service.Command
{
    class JobCommand : BaseCommand
    {
        public override string Command => "JOB";

        public JobCommand()
        {
            _subcommands = new List<ICommand>()
            {
                new JobStatus(),
                new JobStart(),
                new JobStop()
            };
        }

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
