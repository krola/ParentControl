using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParentControl.DTO;
using ParentControl.Infrastructure.Service.Model;
using Timesheet = ParentControl.DTO.Timesheet;

namespace ParentControl.Infrastructure.Contracts.Services
{
    public interface IScheduleService
    {
        Schedule GetDeviceSchedule(string device);

        void AddTimesheet(Timesheet timesheet);

        void RemoveSchedule(Schedule schedule);
    }
}
