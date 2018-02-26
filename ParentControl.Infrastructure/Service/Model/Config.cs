using System.Collections.Generic;
using ParentControl.DTO;

namespace ParentControl.Infrastructure.Service.Model
{
    public class Config
    {
        public AuthenticationData AuthenticationData { get; set; }

        public string ServerAddress { get; set; }

        public bool AllowOnNoTimesheet { get; set; }

        public Device Device { get; set; }

        public List<Timesheet> Timesheets { get; set; }
    }
}
