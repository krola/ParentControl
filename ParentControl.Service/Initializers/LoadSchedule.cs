using ParentControl.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Service.Initializers
{
    class LoadSchedule : BaseInitializer
    {
        public LoadSchedule(BaseInitializer nextProcess) : base(nextProcess)
        {
        }

        protected override string ProcessName => "Loading Schedule";

        protected override bool CanSkip => true;

        protected override void Do()
        {
            var scheduleService = Context.ParentControlService.ScheduleService;
            try
            {
                Context.Schedule = scheduleService.GetScheduleFor(Context.Device.Id).FirstOrDefault();
                if (Context.Schedule == null)
                {
                    throw new Exception("No schedule loaded");
                }

                PrintMessage($"Schedule loaded");
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

            if (Context.Device == null)
            {
                PrintMessage($"Device is missing!", ConsoleColor.Yellow);
                return false;
            }

            return true;
        }
    }
}
