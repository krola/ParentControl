using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParentControl.Server.Models;

namespace ParentControl.Server.Contracts.Repositories
{
    public interface IScheduleRepository
    {
        ScheduleModel CreateSchedule(string name, DeviceModel device);

        ScheduleModel FindScheduleById(int id);
        ScheduleModel FindScheduleByDevice(DeviceModel device);
    }
}
