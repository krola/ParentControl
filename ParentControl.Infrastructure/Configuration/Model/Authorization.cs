using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Infrastructure.Configuration.Model
{
    public class Authorization
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool AutoLogin { get; set; }
    }
}
