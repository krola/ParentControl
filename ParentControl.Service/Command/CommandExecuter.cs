using ParentControl.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Service.Command
{
    class CommandExecuter
    {
        List<ICommand> _commands;

        public CommandExecuter()
        {
            _commands = new List<ICommand>();
            foreach (Type type in Assembly.GetAssembly(typeof(ICommand)).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(BaseCommand)) && myType.Namespace.Equals("ParentControl.Service.Command")))
            {
                _commands.Add((ICommand)Activator.CreateInstance(type));
            }
        }

        public void Execute(string command)
        {
            if (string.IsNullOrEmpty(command))
            {
                return;
            }

            var commands = command.Split();

            if (!_commands.Select(c => c.Command).Any(c => c.Equals(commands.First(), StringComparison.InvariantCultureIgnoreCase)))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid command");
                Console.ForegroundColor = ConsoleColor.White;
            }

            foreach(var commandObj in _commands)
            {
                commandObj.Execute(command);
            }
        }
    }
}
