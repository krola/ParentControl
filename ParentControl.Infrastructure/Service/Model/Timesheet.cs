using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Infrastructure.Service.Model
{
    public class Timesheet
    {
        public int Id { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }

        public DateTime CreateTime { get; set; }

        public TimeSpan TotalTime { get; set; }
    }
}
