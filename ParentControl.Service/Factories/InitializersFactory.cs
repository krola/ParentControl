using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ParentControl.Service.Contracts;
using System.Threading.Tasks;
using System.Configuration;
using ParentControl.Service.Configuration;
using ParentControl.Service.Initializers;

namespace ParentControl.Service.Factories
{
    class InitializersFactory
    {
        public static BaseInitializer CreateProcessPipline()
        {
            //ToDo check for duplicates
            var section = ConfigurationManager.GetSection("ParentControl");
            object last = null;
            if (section != null)
            {
                var processes = (section as ParentControlConfiguration).Processes;
                Console.WriteLine($"Creating {processes.Count} initializers");

                for (int i = processes.Count - 1; i >= 0; i--)
                {
                    var type = Type.GetType(processes[i].Type);
                    Object[] args = { last };
                    var instance = Activator.CreateInstance(type, args);
                    last = instance;
                }
            }

            return last as BaseInitializer;
        }

        public static BaseInitializer CreateInitializer(string name)
        {
            return CreateInitializer(name, null);
        }

        public static BaseInitializer CreateInitializer(string name, BaseInitializer next)
        {
            var allTypes = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
            var wantedType = allTypes.FirstOrDefault(t => t.Name.Equals(name) && t.BaseType.Equals(typeof(BaseInitializer)));
            if (wantedType != null)
            {
                var initializer = Activator.CreateInstance(wantedType,new object[] { next });
                return initializer as BaseInitializer;
            }
            return null;
        }
    }
}
