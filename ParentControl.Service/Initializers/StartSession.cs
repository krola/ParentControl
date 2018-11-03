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

        protected override bool CanSkip => true;

        protected override bool Valid()
        {
            if(Context.TimeLeft == TimeSpan.Zero || Context.TimeLeft < TimeSpan.Zero)
            {
                return false;
            }

            if (Context.ActiveSession != null)
            {
                return false;
            }

            return true;
        }

        protected override void Do()
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

            Context.ParentControlService.LocalSessionTracker.SaveSession(Context.ActiveSession);
        }
    }
}
