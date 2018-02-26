using System.Collections.Generic;
using ParentControl.DTO;

namespace ParentControl.Infrastructure.Contracts.Services
{
    public interface ILocalSessionTracker
    {
        void SaveSession(Session session);

        void RemoveSession(Session session);

        IList<Session> Sessions { get; }
    }
}
