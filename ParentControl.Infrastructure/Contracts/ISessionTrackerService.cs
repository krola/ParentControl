using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParentControl.DTO;

namespace ParentControl.Infrastructure.Contracts
{
    public interface ISessionTrackerService
    {
        void SaveSession(Session session);

        void RemoveSession(Service.Model.Session session);

        IList<Service.Model.Session> Sessions { get; }
    }
}
