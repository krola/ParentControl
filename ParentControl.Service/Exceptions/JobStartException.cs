using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Service.Exceptions
{
    class JobStartException : Exception
    {
        public JobStartException(string message) : base(message)
        {
        }
    }
}
