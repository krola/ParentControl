using ParentControl.DTO;
using ParentControl.Service.Constants;
using ParentControl.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Service.Initializers
{
    class StartSession : BaseInitializer
    {
        public StartSession(BaseInitializer nextProcess) : base(nextProcess)
        {
        }

        protected override string ProcessName => "StartSession";

        protected override bool CanSkip => false;

        protected override bool Valid()
        {
            return true;
        }

        protected override void Do()
        {
            Context.TodaySessions = Context.ParentControlService.LocalSessionTracker.Sessions?.Where(
                s => s.SessionStart.Date == DateTime.UtcNow.Date).ToList();
            if (Context.TodaySessions == null || Context.TodaySessions.All(s => s.SessionEnd != null))
            {
                if (Context.Mode == Mode.Offline)
                {
                    Context.ActiveSession = new Session()
                    {
                        SessionStart = DateTime.UtcNow,
                        Id = Guid.NewGuid(),
                        Device = Context.ParentControlService.ConfigService.Config.Device
                    };
                }
                else
                {
                    Context.ActiveSession = Context.ParentControlService.SessionService.StartSession(Context.Device.Id);
                }
            }
            else
            {
                Context.ActiveSession = Context.TodaySessions.First(s => s.SessionEnd == null);
            }

            Context.ParentControlService.LocalSessionTracker.SaveSession(Context.ActiveSession);
        }
    }
}
