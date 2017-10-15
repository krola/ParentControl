using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Client.Logger.Sources
{
    public interface ISource
    {
        void Write(string message);
    }
}
