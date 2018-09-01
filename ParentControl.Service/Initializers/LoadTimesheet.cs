using ParentControl.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Service.Initializers
{
    class LoadTimesheet : BaseInitializer
    {
        public LoadTimesheet(BaseInitializer nextProcess) : base(nextProcess)
        {
        }

        protected override string ProcessName => "Loading Timesheets";

        protected override bool CanSkip => true;

        protected override void Do()
        {
            var timesheetService = Context.ParentControlService.TimesheetService;
            try
            {
                var timesheets = timesheetService.GetTimesheetFor(Context.Schedule.Id).ToList();
                Context.ParentControlService.ConfigService.SaveTimesheets(timesheets, Context.Schedule.AllowWitoutTimesheet);
                PrintMessage($"Timesheet loaded");
            }
            catch (Exception e)
            {
                var message = $"Error {e.Message}";
                throw new Exception(message);
            }
        }

        protected override bool Valid()
        {
            if (Context.Mode == Constants.Mode.Offline)
            {
                return false;
            }

            if(Context.Schedule == null)
            {
                PrintMessage($"Schedule is missing!", ConsoleColor.Yellow);
                return false;
            }

            return true;
        }
    }
}
