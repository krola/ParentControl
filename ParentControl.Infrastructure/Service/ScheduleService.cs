using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using ParentControl.DTO;
using ParentControl.Infrastructure.Contracts.Services;
using ParentControl.Infrastructure.Service.Model;

namespace ParentControl.Infrastructure.Service
{
    internal class ScheduleService : BaseService, IScheduleService
    {
        public IEnumerable<Schedule> GetScheduleFor(int deviceId)
        {
            Schedule scheduleDto = null;
            var scheduleJson = HttpService.GetRequest("/api/Schedule", new RequestParameter[] { new RequestParameter()
                {
                    Key = "DeviceId",
                    Value = deviceId.ToString()
                } 
            });

            return JsonConvert.DeserializeObject<IEnumerable<Schedule>>(scheduleJson);
        }

        public ScheduleService(IHttpService httpService) : base(httpService)
        {
        }
    }
}
