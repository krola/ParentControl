using System.Collections.Generic;
using ParentControl.DTO;
using ParentControl.Infrastructure.Service.Model;

namespace ParentControl.Infrastructure.Contracts
{
    public interface IConfigService
    {
        void SaveAuthentication(AuthenticationData authenticationData);

        DeviceConfiguration Config { get; }

        void SaveServerAddress(string server);

        void SaveDevice(string deviceId, string deviceName);

        void SaveTimesheets(List<Timesheet> timesheets, bool allowWitoutTimesheet);

        string FullPath { get; }
    }
}
