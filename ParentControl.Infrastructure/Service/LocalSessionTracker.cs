using System.Collections.Generic;
using System.Linq;
using ParentControl.Infrastructure.Storage;
using ParentControl.Infrastructure.Contracts.Services;
using Session = ParentControl.DTO.Session;

namespace ParentControl.Infrastructure.Service
{
    public class LocalSessionTracker : LocalStorageBase<List<Session>>, ILocalSessionTracker
    {
        public void SaveSession(Session session)
        {
            var existingSession = Store.FirstOrDefault(s => s.Id.Equals(session.Id));

            if (existingSession == null)
            {
                Store.Add(new Session()
                {
                    Id = session.Id,
                    SessionStart = session.SessionStart,
                    SessionEnd = session.SessionEnd
                });
            }
            else
            {
                existingSession.SessionStart = session.SessionStart;
                existingSession.SessionEnd = session.SessionEnd;
            }

            SaveStore();
        }

        public void RemoveSession(Session session)
        {
            var foundSession = Sessions.FirstOrDefault(s => s.Id == session.Id);
            Sessions.Remove(foundSession);
            SaveStore();
        }

        public IList<Session> Sessions { get { return Store; } }

        public LocalSessionTracker() : base("Sessions")
        {
        }
    }
}
