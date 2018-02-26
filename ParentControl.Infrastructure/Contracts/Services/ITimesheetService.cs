using System.Collections.Generic;
using ParentControl.DTO;

namespace ParentControl.Infrastructure.Contracts
{
    public interface ITimesheetService
    {
        IEnumerable<Timesheet> GetTimesheetFor(int scheduleId);
    }
}
