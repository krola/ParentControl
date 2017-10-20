using System.Collections.Generic;
using ParentControl.DTO;

namespace ParentControl.Infrastructure.Contracts.Services
{
    public interface ISessionTrackerService
    {
        void SaveSession(Session session);

        void RemoveSession(Service.Model.Session session);

        IList<Service.Model.Session> Sessions { get; }
    }
}
