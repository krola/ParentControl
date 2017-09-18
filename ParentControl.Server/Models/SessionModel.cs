using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace ParentControl.Server.Models
{
    public class SessionModel
    {
        [Key]
        public int Id { get; set; }
        public Guid UniqueId { get; set; }

        public DateTime StarTime { get; set; }

        public DateTime? EndTime { get; set; }

        public virtual DeviceModel Device { get; set; }
    }
}