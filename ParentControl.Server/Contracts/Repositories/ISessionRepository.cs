using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParentControl.Server.Models;

namespace ParentControl.Server.Contracts.Repositories
{
    public interface ISessionRepository
    {
        IEnumerable<SessionModel> GetSessionForDate(DateTime date, DeviceModel device);

        SessionModel AddSession(SessionModel session);

        void EndSession(Guid uniqueId, DateTime? endTime);
        SessionModel GetSessionByID(Guid sessionSessionId);
        void Update();
    }
}
