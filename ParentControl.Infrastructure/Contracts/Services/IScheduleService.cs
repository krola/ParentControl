using System.Collections.Generic;
using ParentControl.DTO;
using Timesheet = ParentControl.DTO.Timesheet;

namespace ParentControl.Infrastructure.Contracts.Services
{
    public interface IScheduleService
    {
        IEnumerable<Schedule> GetScheduleFor(int deviceId);

        //void AddTimesheet(Timesheet timesheet);
    }
}
