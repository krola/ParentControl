using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParentControl.Infrastructure.Contracts;

namespace ParentControl.Infrastructure.Configuration
{
    public class ClientConfiguration : IConfiguration
    {
        public string ApiAddress { get; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool AutoLogin { get; set; }
    }
}
