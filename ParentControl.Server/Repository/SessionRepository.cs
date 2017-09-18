using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using ParentControl.Server.Contracts;
using ParentControl.Server.Contracts.Repositories;
using ParentControl.Server.Models;

namespace ParentControl.Server.Repository
{
    public class SessionRepository : ISessionRepository
    {
        private IAppContext _appContext;
        public SessionRepository(IAppContext appContext)
        {
            _appContext = appContext;
        }

        public IEnumerable<SessionModel> GetSessionForDate(DateTime date, DeviceModel device)
        {
            return _appContext.Session.Where(s => s.Device.DeviceId == device.DeviceId && EntityFunctions.TruncateTime(s.StarTime) == EntityFunctions.TruncateTime(date.Date)).ToList();
        }

        public SessionModel AddSession(SessionModel session)
        {
            _appContext.Session.Add(session);
            _appContext.SaveChanges();
            return session;
        }

        public void EndSession(Guid uniqueId, DateTime? endTime)
        {
            var session = _appContext.Session.First(s => s.UniqueId.Equals(uniqueId));
            session.EndTime = endTime;
            _appContext.SaveChanges();
        }

        public SessionModel GetSessionByID(Guid sessionSessionId)
        {
            return _appContext.Session.FirstOrDefault(s => s.UniqueId.Equals(sessionSessionId));
        }

        public void Update()
        {
            _appContext.SaveChanges();
        }
    }
}