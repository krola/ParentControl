using System;

namespace ParentControl.DTO
{
    public class Timesheet
    {
        public int ScheduleId { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
