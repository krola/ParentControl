using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Service.Contracts
{
    interface IWebsocketRequestSolver
    {
        string Command { get; }
        string Handle(string payload);
    }
}
