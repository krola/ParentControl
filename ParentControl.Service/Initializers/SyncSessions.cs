using ParentControl.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Service.Initializers
{
    class SyncSessions : BaseInitializer
    {
        public SyncSessions(BaseInitializer nextProcess) : base(nextProcess)
        {
        }

        protected override string ProcessName => "SyncSessions";

        protected override bool CanSkip => false;

        protected override void Do()
        {
            var localSessions = Context.ParentControlService.LocalSessionTracker.Sessions.Where(d => d.SessionStart.Date == DateTime.UtcNow.Date).ToList();
            var remoteSessions = Context.ParentControlService.SessionService.TodaySessions(Context.Device.Id);

            foreach (var remoteSession in remoteSessions)
            {
                Context.ParentControlService.LocalSessionTracker.SaveSession(remoteSession);
            }

            foreach (var localSession in localSessions)
            {
                Context.ParentControlService.SessionService.UpdateSession(localSession, Context.Device.Id);
            }
        }

        protected override bool Valid()
        {
            if(Context.Mode != Constants.Mode.Online)
            {
                return false;
            }

            if(Context.ParentControlService.LocalSessionTracker == null)
            {
                PrintMessage("LocalSessionTracker is not avaiable.");
                return false;
            }

            if (Context.Device == null)
            {
                PrintMessage("Device is not avaiable.");
                return false;
            }

            return true;
        }
    }
}
