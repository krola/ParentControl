using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParentControl.DTO;
using ParentControl.Core.Services.Model;
using Session = ParentControl.DTO.Session;

namespace ParentControl.Core.Contracts.Services
{
    public interface ISessionService
    {
        Session StartSession(string deviceId);

        Session EndSession(Session session);
        Task<IEnumerable<Session>> TodaySessionsAsync(string deviceId);
        void UpdateSession(Session session, string deviceId);
    }
}
