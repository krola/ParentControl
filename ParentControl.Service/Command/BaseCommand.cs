using ParentControl.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParentControl.Service.Command
{
    abstract class BaseCommand : ICommand
    {
        protected App Context = App.Context;

        public abstract string Command { get; }

        protected List<ICommand> _subcommands;

        public void Execute(string command) {
            if(string.IsNullOrEmpty(command)) {
                return;
            }

            var splitedCommand = command.Split();

            if (!splitedCommand[0].Equals(Command, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            if(splitedCommand.Length > 1 && _subcommands != null  && _subcommands.Any(s => s.Command.Equals(splitedCommand[1], StringComparison.InvariantCultureIgnoreCase)))
            {
                _subcommands.First(s => s.Command.Equals(splitedCommand[1], StringComparison.InvariantCultureIgnoreCase)).Execute(string.Join(" ",splitedCommand.Skip(1)));
                return;
            }

            Do(splitedCommand.Skip(1).ToArray());
        }

        public abstract void PrintInfo();

        protected abstract void Do(string[] args);
    }
}
