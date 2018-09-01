using System;
using System.Linq;

namespace ParentControl.Service.Initializers
{
    class CalculateTime : BaseInitializer
    {
        public CalculateTime(BaseInitializer nextProcess) : base(nextProcess)
        {
        }

        protected override string ProcessName => "CalculateTime";

        protected override bool CanSkip => false;

        protected override void Do()
        {
           
            if(!Context.Schedule.AllowWitoutTimesheet)
            {
                var timesheet = Context.ParentControlService.ConfigService.Config.Timesheets.FirstOrDefault(t => DateTime.UtcNow > t.DateFrom && DateTime.UtcNow < t.DateTo);
                PrintMessage($"Found timesheet. Total time: {timesheet.Time.ToString("h'h 'm'm 's's'")}");
                //session 
                var allTimeSpendToday = Context.TodaySessions != null ? new TimeSpan(Context.TodaySessions.Where(s => s.SessionEnd != null).Sum(s => s.SessionEnd.Value.Subtract(s.SessionStart).Ticks)) : new TimeSpan();
                allTimeSpendToday = allTimeSpendToday.Add(DateTime.UtcNow.Subtract(Context.ActiveSession.SessionStart));
                Context.TimeLeft = timesheet.Time.Subtract(allTimeSpendToday);

                if (Context.TimeLeft < TimeSpan.Zero)
                {
                    PrintMessage($"No time left for today!");
                    Context.TimeLeft = TimeSpan.Zero;
                }
                else
                {
                    PrintMessage($"Time left for today: {Context.TimeLeft:hh\\:mm\\:ss}");
                }
            }
        }

        protected override bool Valid()
        {
            if(Context.Schedule == null)
            {
                PrintMessage($"No schedule configured!");
                return false;
            }

            var timesheet = Context.ParentControlService.ConfigService.Config.Timesheets.FirstOrDefault(t => DateTime.UtcNow > t.DateFrom && DateTime.UtcNow < t.DateTo);
            if (timesheet == null && !Context.Schedule.AllowWitoutTimesheet)
            {
                PrintMessage($"No timesheet for today!");
                return false;
            }

            return true;
        }
    }
}
