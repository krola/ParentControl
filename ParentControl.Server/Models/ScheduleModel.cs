using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ParentControl.Server.Models
{
    public class ScheduleModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool AllowWithNoTimesheet { get; set; }

        public virtual DeviceModel Device { get; set; }
    }
}