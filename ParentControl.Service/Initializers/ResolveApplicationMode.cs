using ParentControl.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Service.Initializers
{
    class ResolveApplicationMode : BaseInitializer
    {
        public ResolveApplicationMode(BaseInitializer nextProcess) : base(nextProcess)
        {
        }

        protected override string ProcessName => "Resolving Application Mode";

        protected override bool CanSkip => false;

        protected override void Do()
        {
            Context.Mode = Context.ParentControlService.IsConnected ? Constants.Mode.Online : Constants.Mode.Offline;
            PrintMessage($"Mode: {Context.Mode.ToString()}");
        }

        protected override bool Valid()
        {
            if(Context.ParentControlService.ValidateApiConnection() == false)
            {
                PrintMessage($"Missing configuration", ConsoleColor.Red);
                return false;
            }
            return true;
        }
    }
}
