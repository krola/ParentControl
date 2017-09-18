using System.Collections.Generic;

namespace ParentControl.DTO
{
    public class Schedule
    {
        public int Id { get; set; }
        public bool AllowWitoutTimesheet { get; set; }

        public string Name { get; set; }

        public IEnumerable<Timesheet> Timesheets { get; set; } 
    }
}
