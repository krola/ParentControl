using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ParentControl.Server.Models
{
    public class TimesheetModel
    {
        [Key]
        public int Id { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public TimeSpan Time { get; set; }

        public DateTime CreateTime { get; set; }

        public virtual ScheduleModel Schedule { get; set; }
    }
}