using System.Collections.Generic;
using System.Linq;
using ParentControl.Core.Storage;
using ParentControl.Core.Contracts.Services;
using Model = ParentControl.Core.Services.Model;
using Session = ParentControl.DTO.Session;

namespace ParentControl.Core.Service
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

            UpdateConfigAsync();
        }

        public void RemoveSession(Model.Session session)
        {
            var foundSession = Sessions.FirstOrDefault(s => s.SessionId == session.SessionId);
            Sessions.Remove(foundSession);
            UpdateConfigAsync();
        }

        public IList<Model.Session> Sessions { get { return Store; } }

        public SessionTrackerService() : base("Sessions")
        {
        }
    }
}
