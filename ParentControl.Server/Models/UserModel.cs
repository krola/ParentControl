using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace ParentControl.Server.Models
{
    public class UserModel
    {
        [Key]
        public int Id { get; set; }
      
        public string UserId { get; set; }

        public string Name { get; set; }
    }
}