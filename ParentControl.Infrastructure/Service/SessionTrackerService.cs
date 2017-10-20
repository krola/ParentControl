using System.Collections.Generic;
using System.Linq;
using ParentControl.Infrastructure.Storage;
using ParentControl.Infrastructure.Contracts.Services;
using ParentControl.Infrastructure.Service.Model;
using Session = ParentControl.DTO.Session;

namespace ParentControl.Infrastructure.Service
{
    public class SessionTrackerService : LocalStorageBase<List<Model.Session>>, ISessionTrackerService
    {
        public void SaveSession(Session session)
        {
            var existingSession = Store.FirstOrDefault(s => s.SessionId.Equals(session.SessionId));

            if (existingSession == null)
            {
                Store.Add(new Model.Session()
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

        public IList<Model.Session> Sessions { get { return Store; } }

        public SessionTrackerService() : base("Sessions")
        {
        }
    }
}
