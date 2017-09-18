using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ParentControl.Server.Contracts;
using ParentControl.Server.Contracts.Repositories;
using ParentControl.Server.Models;

namespace ParentControl.Server.Repository
{
    public class TimesheetRepository : ITimesheetRepository
    {
        private IAppContext _appContext;
        public TimesheetRepository(IAppContext appContext)
        {
            _appContext = appContext;
        }
        public TimesheetModel CreateTimesheet(TimesheetModel model)
        {
            _appContext.Timesheets.Add(model);

            _appContext.SaveChanges();

            return model;
        }

        public IEnumerable<TimesheetModel> GetTimesheets(ScheduleModel Schedule)
        {
            return _appContext.Timesheets.Where(s => s.Schedule.Id == Schedule.Id).ToList();
        }
    }
}