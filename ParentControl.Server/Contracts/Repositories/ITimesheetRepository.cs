using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParentControl.Server.Models;

namespace ParentControl.Server.Contracts.Repositories
{
    public interface ITimesheetRepository
    {
        TimesheetModel CreateTimesheet(TimesheetModel model);

        IEnumerable<TimesheetModel> GetTimesheets(ScheduleModel Schedule);
    }
}
