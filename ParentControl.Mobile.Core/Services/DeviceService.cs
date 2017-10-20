using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParentControl.Core.Contracts.Services;
using ParentControl.Core.Helpers;
using ParentControl.Core.Services.Model;
using Device = ParentControl.Core.Services.Model.Device;

namespace ParentControl.Core.Services
{
    public class DeviceService : BaseService, IDeviceService
    {
        public DeviceService(IHttpService httpService) : base(httpService)
        {
        }

        public async Task<IEnumerable<DTO.Device>> GetDevicesAsync()
        {
            var result = await HttpService.GetRequestAsync("/api/Device/GetDevices");
            return JsonConvert.DeserializeObject<List<DTO.Device>>(result);
        }

        public string GenerateDeviceId()
        {
            var deviceKey = "";// Environment.MachineName + "_" + System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            return deviceKey.HashString();
        }

        public string GenerateDeviceId(string username)
        {
            throw new NotImplementedException();
        }

        public void RegisterDevice(Device device)
        {
            HttpService.PostRequestAsync("/api/Device/Register", new RequestParameter[] {
                new RequestParameter
                {
                    Key = "DeviceId",
                    Value = device.DeviceId
                },
                new RequestParameter
                {
                    Key = "Name",
                    Value = device.DeviceName
                }, 
            });
        }
    }
}
