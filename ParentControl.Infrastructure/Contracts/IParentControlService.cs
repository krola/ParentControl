using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParentControl.Infrastructure.Contracts.Services;
using ParentControl.Infrastructure.Service;

namespace ParentControl.Infrastructure.Contracts
{
    public interface IParentControlService
    {
        IConfigService ConfigService { get; }
        IDeviceService DeviceService { get; }
        IScheduleService ScheduleService { get; }

        ISessionService SessionService { get; }

        bool IsConnected { get; }
        string InfoData { get; }
    }
}
