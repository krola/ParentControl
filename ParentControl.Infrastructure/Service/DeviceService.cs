using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParentControl.DTO;
using ParentControl.Infrastructure.Contracts;
using ParentControl.Infrastructure.Contracts.Services;
using ParentControl.Infrastructure.Helpers;
using ParentControl.Infrastructure.Owin.Model;
using ParentControl.Infrastructure.Service.Model;
using Device = ParentControl.Infrastructure.Service.Model.Device;

namespace ParentControl.Infrastructure.Service
{
    public class DeviceService : BaseService, IDeviceService
    {
        public DeviceService(IHttpService httpService) : base(httpService)
        {
        }

        public IEnumerable<DTO.Device> GetDevices()
        {
            var result = HttpService.GetRequest("/api/Device/GetDevices");
            return JsonConvert.DeserializeObject<List<DTO.Device>>(result);
        }

        public string GenerateDeviceId()
        {
            var deviceKey = Environment.MachineName + "_" + System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            return deviceKey.HashString();
        }

        public string GenerateDeviceId(string username)
        {
            throw new NotImplementedException();
        }

        public void RegisterDevice(Device device)
        {
            HttpService.PostRequest("/api/Device/Register", new RequestParameter[] {
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
