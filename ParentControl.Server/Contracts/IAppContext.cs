using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ParentControl.Server.Models;

namespace ParentControl.Server.Contracts
{
    public interface IAppContext
    {
        DbSet<DeviceModel> Devices { get; set; }
        DbSet<ScheduleModel> Schedules { get; set; }
        DbSet<TimesheetModel> Timesheets { get; set; }
        DbSet<SessionModel> Session { get; set; }
        DbSet<UserModel> Users { get; set; }

        void SaveChanges();

    }
}