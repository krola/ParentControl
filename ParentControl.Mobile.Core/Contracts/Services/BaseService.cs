using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Core.Contracts.Services
{
    public class BaseService
    {
        protected IHttpService HttpService;

        public BaseService(IHttpService httpService)
        {
            HttpService = httpService;
        }
    }
}
