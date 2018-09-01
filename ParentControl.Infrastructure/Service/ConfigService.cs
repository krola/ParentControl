using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using ParentControl.DTO;
using ParentControl.Infrastructure.Contracts;
using ParentControl.Infrastructure.Service.Model;
using ParentControl.Infrastructure.Storage;

namespace ParentControl.Infrastructure.Service
{
    public class ConfigService : LocalStorageBase<DeviceConfiguration>, IConfigService
    {
        

        public ConfigService() : base("Config")
        {
        }

        public void SaveAuthentication(AuthenticationData authenticationData)
        {
            throw new System.NotImplementedException();
        }

        public DeviceConfiguration Config => Store;

        public void SaveServerAddress(string server)
        {
            throw new System.NotImplementedException();
        }

        public void SaveDevice(string deviceId, string deviceName)
        {
            throw new System.NotImplementedException();
        }

        public void SaveTimesheets(List<Timesheet> timesheets, bool allowWitoutTimesheet)
        {
            Store.Timesheets = timesheets;
            Store.AllowOnNoTimesheet = allowWitoutTimesheet;
            SaveStore();
        }
    }
}
