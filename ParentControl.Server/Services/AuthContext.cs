using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using ParentControl.Server.Models;

namespace ParentControl.Server.Services
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
           : base("AuthContext")
        {

        }
    }
}