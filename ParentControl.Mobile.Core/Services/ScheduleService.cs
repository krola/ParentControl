using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParentControl.DTO;
using ParentControl.Core.Contracts;
using ParentControl.Core.Contracts.Services;
using ParentControl.Core.Services.Model;
using Timesheet = ParentControl.DTO.Timesheet;

namespace ParentControl.Core.Service
{
    public class ScheduleService : BaseService, IScheduleService
    {
        public async Task<Schedule> GetDeviceScheduleAsync(string device)
        {
            Schedule schedule = null;
            var scheduleJson = await HttpService.GetRequestAsync("/api/Schedule/GetSchedule", new RequestParameter[] { new RequestParameter()
            {
                Key = "deviceId",
                Value = device
            } });

            var timesheetsJson = await HttpService.GetRequestAsync("/api/Timesheet/GetDeviceTimsheets", new RequestParameter[] { new RequestParameter()
            {
                Key = "deviceId",
                Value = device
            } });

            var timesheets = JsonConvert.DeserializeObject<IEnumerable<Timesheet>>(timesheetsJson);
            schedule = JsonConvert.DeserializeObject<Schedule>(scheduleJson);
            schedule.Timesheets = timesheets;
            return schedule;
        }

        public async Task AddTimesheetAsync(Timesheet timesheet)
        {
            var scheduleJson = await HttpService.PostRequestAsync("/api/Timesheet/CreateTimesheet", new RequestParameter[] {
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
