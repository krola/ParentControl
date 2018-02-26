using System.Collections.Generic;
using Newtonsoft.Json;
using ParentControl.Infrastructure.Contracts.Services;
using ParentControl.Infrastructure.Service.Model;

namespace ParentControl.Infrastructure.Service
{
    internal class DeviceService : BaseService, IDeviceService
    {
        private const string ApiEndpoint = "/api/Device";

        public DeviceService(IHttpService httpService) : base(httpService)
        {
        }

        public IEnumerable<DTO.Device> GetDevices()
        {
            var result = HttpService.GetRequest(ApiEndpoint);
            return JsonConvert.DeserializeObject<List<DTO.Device>>(result);
        }

        public void RegisterDevice(string deviceName)
        {
            HttpService.PostRequest(ApiEndpoint, new RequestParameter[] {
                new RequestParameter
                {
                    Key = "name",
                    Value = deviceName
                }, 
            });
        }
    }
}
