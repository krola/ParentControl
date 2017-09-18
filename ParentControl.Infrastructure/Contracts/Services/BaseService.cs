using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Infrastructure.Contracts.Services
{
    public class BaseService
    {
        protected IOwinHandler _owinHandler;

        public BaseService(IOwinHandler owinHandler)
        {
            _owinHandler = owinHandler;
        }
    }
}
