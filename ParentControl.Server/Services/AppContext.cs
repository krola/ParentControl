using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ParentControl.Server.Contracts;
using ParentControl.Server.Models;

namespace ParentControl.Server.Services
{
    public class AppContext : DbContext, IAppContext
    {
        public AppContext()
           : base("AppContext")
        {

        }

        public DbSet<DeviceModel> Devices { get; set; }
        public DbSet<ScheduleModel> Schedules { get; set; }
        public DbSet<TimesheetModel> Timesheets { get; set; }
        public DbSet<SessionModel> Session { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public void SaveChanges()
        {
            base.SaveChanges();
        }
    }
}