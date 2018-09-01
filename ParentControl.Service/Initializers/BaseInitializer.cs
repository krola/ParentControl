using System;

namespace ParentControl.Service.Initializers
{
    abstract class BaseInitializer
    {
        private readonly BaseInitializer _nextProcess;

        protected App Context = App.Context;

        public BaseInitializer() : this(null)
        {

        }

        public BaseInitializer(BaseInitializer nextProcess)
        {
            _nextProcess = nextProcess;
        }

        protected abstract void Do();
        protected abstract string ProcessName { get; }
        protected abstract bool CanSkip { get; }

        private string ValidationFailMessage = "Validation failed!";

        public void Run()
        {
            if (!Valid() && !CanSkip)
            {
                PrintMessage(ValidationFailMessage, ConsoleColor.Red);
                return;
            }

            if (!Valid() && CanSkip)
            {
                if (_nextProcess != null)
                {
                    _nextProcess.Run();
                }
                return;
            }
            PrintMessage(ProcessName, ConsoleColor.Blue);
            Do();

            if(_nextProcess != null)
            {
                _nextProcess.Run();
            }
        }

        protected void PrintMessage(string message)
        {
            PrintMessage(message, ConsoleColor.White);
        }

        protected void PrintMessage(string message, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        protected virtual bool Valid()
        {
            return true;
        }
    }
}
