using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParentControl.DTO;
using ParentControl.Infrastructure.Contracts;
using ParentControl.Infrastructure.Service.Model;
using Device = ParentControl.Infrastructure.Service.Model.Device;
using Timesheet = ParentControl.Infrastructure.Service.Model.Timesheet;

namespace ParentControl.Infrastructure.Service
{
    public class ConfigService : BaseConfig<Config>, IConfigService
    {
        public void SaveAuthentication(AuthenticationData authenticationData)
        {
            Config.AuthenticationData = authenticationData;
            UpdateConfig();
        }

        public void SaveServerAddress(string server)
        {
            Config.ServerAddress = server;
            UpdateConfig();
        }

        public void SaveDevice(string deviceId, string deviceName)
        {
            if (Config.Device == null)
            {
                Config.Device = new Device()
                {
                    DeviceName = deviceName,
                    DeviceId = deviceId
                };
            }
            else
            {
                Config.Device.DeviceId = deviceId;
                Config.Device.DeviceName = deviceName;
            }

            UpdateConfig();
        }

        public void SaveTimesheets(List<Timesheet> timesheets, bool allowWitoutTimesheet)
        {

            Config.Timesheets = timesheets;
            Config.AllowOnNoTimesheet = allowWitoutTimesheet;
            UpdateConfig();
        }

        public ConfigService() : base("Config")
        {
        }
    }
}
