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
            if (Context.Mode == Constants.Mode.Online && Context.Schedule.AllowWitoutTimesheet)
            {
                return;
            }

            if (Context.Mode == Constants.Mode.Offline && Context.ParentControlService.ConfigService.Config.AllowOnNoTimesheet)
            {
                return;
            }

            var timesheet = Context.ParentControlService.ConfigService.Config.Timesheets
                                            .Where(t => DateTime.UtcNow > t.DateFrom && DateTime.UtcNow < t.DateTo)
                                            .OrderByDescending(t => t.CreateTime)
                                            .FirstOrDefault();
            if (timesheet == null)
            {
                PrintMessage($"Timesheet not found", ConsoleColor.Yellow);
                Context.TimeLeft = TimeSpan.Zero;
                return;
            }

            CalculateAndSetTimeLeft(timesheet);
        }

        private void CalculateAndSetTimeLeft(DTO.Timesheet timesheet)
        {
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
}
