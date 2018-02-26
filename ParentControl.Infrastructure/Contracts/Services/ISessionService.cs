using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParentControl.DTO;
using ParentControl.Infrastructure.Service.Model;
using Session = ParentControl.DTO.Session;

namespace ParentControl.Infrastructure.Contracts.Services
{
    public interface ISessionService
    {
        Session StartSession(int deviceId);

        Session EndSession(Session session);
        IEnumerable<Session> TodaySessions(int deviceId);
        void UpdateSession(Session session, int deviceId);
    }
}
