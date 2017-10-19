using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParentControl.DTO;
using ParentControl.Infrastructure.Contracts;
using ParentControl.Infrastructure.Contracts.Services;
using ParentControl.Infrastructure.Owin.Model;

namespace ParentControl.Infrastructure.Service
{
    public class ScheduleService : BaseService, IScheduleService
    {
        public Schedule GetDeviceSchedule(string device)
        {
            Schedule schedule = null;
            var scheduleJson = HttpService.GetRequest("/api/Schedule/GetSchedule", new RequestParameter[] { new RequestParameter()
            {
                Key = "deviceId",
                Value = device
            } });

            var timesheetsJson = HttpService.GetRequest("/api/Timesheet/GetDeviceTimsheets", new RequestParameter[] { new RequestParameter()
            {
                Key = "deviceId",
                Value = device
            } });

            var timesheets = JsonConvert.DeserializeObject<IEnumerable<Timesheet>>(timesheetsJson);
            schedule = JsonConvert.DeserializeObject<Schedule>(scheduleJson);
            schedule.Timesheets = timesheets;
            return schedule;
        }

        public void AddTimesheet(Timesheet timesheet)
        {
            var scheduleJson = HttpService.PostRequest("/api/Timesheet/CreateTimesheet", new RequestParameter[] {
                new RequestParameter()
            {
                Key = "DateFrom",
                Value = timesheet.DateFrom.ToString(CultureInfo.InvariantCulture)
            },
            new RequestParameter()
            {
                Key = "DateTo",
                Value = timesheet.DateTo?.ToString()
            },
            new RequestParameter()
            {
                Key = "Time",
                Value = timesheet.Time.ToString()
            },
            new RequestParameter()
            {
                Key = "ScheduleId",
                Value = timesheet.ScheduleId.ToString()
            }});
        }

        public void RemoveSchedule(Schedule schedule)
        {
            throw new NotImplementedException();
        }

        public ScheduleService(IHttpService httpService) : base(httpService)
        {
        }
    }
}
