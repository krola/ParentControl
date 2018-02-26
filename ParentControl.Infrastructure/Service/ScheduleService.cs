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

        //public void AddTimesheet(Timesheet timesheet)
        //{
        //    var scheduleJson = HttpService.PostRequest("/api/Timesheet/CreateTimesheet", new RequestParameter[] {
        //        new RequestParameter()
        //    {
        //        Key = "DateFrom",
        //        Value = timesheet.DateFrom.ToString(CultureInfo.InvariantCulture)
        //    },
        //    new RequestParameter()
        //    {
        //        Key = "DateTo",
        //        Value = timesheet.DateTo?.ToString()
        //    },
        //    new RequestParameter()
        //    {
        //        Key = "Time",
        //        Value = timesheet.Time.ToString()
        //    },
        //    new RequestParameter()
        //    {
        //        Key = "ScheduleId",
        //        Value = timesheet.ScheduleId.ToString()
        //    }});
        //}

        public ScheduleService(IHttpService httpService) : base(httpService)
        {
        }
    }
}
