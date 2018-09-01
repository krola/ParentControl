using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Service.Command
{
    class ShowCommand : BaseCommand
    {
        public ShowCommand()
        {
        }

        public override string Command => "SHOW";

        public override void PrintInfo()
        {
            throw new NotImplementedException();
        }

        protected override void Do(string[] args)
        {
            
        }
    }
}
