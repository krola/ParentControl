using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParentControl.DTO;
using ParentControl.Infrastructure.Contracts;
using ParentControl.Infrastructure.Service.Model;
using Session = ParentControl.DTO.Session;

namespace ParentControl.Infrastructure.Service
{
    public class SessionTrackerService : BaseConfig<List<Model.Session>>, ISessionTrackerService
    {
        public void SaveSession(Session session)
        {
            var existingSession = Config.FirstOrDefault(s => s.SessionId.Equals(session.SessionId));

            if (existingSession == null)
            {
                Config.Add(new Model.Session()
                {
                    SessionId = session.SessionId,
                    SessionStart = session.SessionStart,
                    SessionEnd = session.SessionEnd
                });
            }
            else
            {
                existingSession.SessionStart = session.SessionStart;
                existingSession.SessionEnd = session.SessionEnd;
            }

            UpdateConfig();
        }

        public void RemoveSession(Model.Session session)
        {
            var foundSession = Sessions.FirstOrDefault(s => s.SessionId == session.SessionId);
            Sessions.Remove(foundSession);
            UpdateConfig();
        }

        public IList<Model.Session> Sessions { get { return Config; } }

        public SessionTrackerService() : base("Sessions")
        {
        }
    }
}
