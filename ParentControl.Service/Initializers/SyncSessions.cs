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

        protected override bool CanSkip => true;

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

            Context.TodaySessions = Context.ParentControlService.LocalSessionTracker.Sessions?.Where(
                s => s.SessionStart.Date == DateTime.UtcNow.Date).ToList();

            if (Context.TodaySessions != null && Context.TodaySessions.Any(s => s.SessionEnd == null))
            {
                Context.ActiveSession = Context.TodaySessions.First(s => s.SessionEnd == null);
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
