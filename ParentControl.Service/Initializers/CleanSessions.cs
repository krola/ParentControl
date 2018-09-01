using ParentControl.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Service.Initializers
{
    class CleanSessions : BaseInitializer
    {
        public CleanSessions(BaseInitializer nextProcess) : base(nextProcess)
        {
        }

        protected override string ProcessName => "CleanSessions";

        protected override bool CanSkip => false;

        protected override void Do()
        {
            var weekBefore = DateTime.UtcNow.Subtract(new TimeSpan(7, 0, 0, 0));
            Context.ParentControlService.LocalSessionTracker.Sessions.Where(d => d.SessionStart.Date < weekBefore.Date).ToList().ForEach(
                s =>
                {
                    Context.ParentControlService.LocalSessionTracker.RemoveSession(s);
                });
        }
    }
}
