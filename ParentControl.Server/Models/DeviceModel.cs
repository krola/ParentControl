using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ParentControl.Server.Models
{
    public class DeviceModel
    {
        [Key]
        public int Id { get; set; }

        public string DeviceId { get; set; }
        public string Name { get; set; }

        public UserModel User { get; set; }
    }
}