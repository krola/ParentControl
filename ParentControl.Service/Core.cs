using ParentControl.Infrastructure.Service;
using System;
using ParentControl.Service.Contracts;
using ParentControl.Service.Factories;
using ParentControl.Service.Command;
using ParentControl.Service.Initializers;

namespace ParentControl.Service
{
    class Core
    {
        private BaseInitializer _initializers;

        public Core()
        {
            
        }

        public void Init()
        {
            _initializers = InitializersFactory.CreateProcessPipline();
        }

        public void Run()
        {
            try
            {
                _initializers.Run();
                App.Context.JobManager.Start();
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
        }
    }
}
