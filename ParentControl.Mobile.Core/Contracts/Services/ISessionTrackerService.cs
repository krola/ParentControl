using System.Collections.Generic;
using ParentControl.DTO;

namespace ParentControl.Core.Contracts.Services
{
    public interface ISessionTrackerService
    {
        void SaveSession(Session session);

        void RemoveSession(Core.Services.Model.Session session);

        IList<Core.Services.Model.Session> Sessions { get; }
    }
}
