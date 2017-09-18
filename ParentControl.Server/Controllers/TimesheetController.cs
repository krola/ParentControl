using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ParentControl.DTO;
using ParentControl.Server.Contracts.Repositories;
using ParentControl.Server.DTO;
using ParentControl.Server.Mappers;
using ParentControl.Server.Models;

namespace ParentControl.Server.Controllers
{
    [Authorize]
    public class TimesheetController : BaseController
    {
        private ITimesheetRepository _timesheetRepository;
        private IScheduleRepository _scheduleRepository;
        private IDeviceRepository _deviceRepository;
        public TimesheetController(IUserRepository userRepository, ITimesheetRepository timesheetRepository, IScheduleRepository scheduleRepository, IDeviceRepository deviceRepository) : base(userRepository)
        {
            _timesheetRepository = timesheetRepository;
            _scheduleRepository = scheduleRepository;
            _deviceRepository = deviceRepository;
        }

        [HttpPost]
        public IHttpActionResult CreateTimesheet(Timesheet newTimesheet)
        {
            var schedule = _scheduleRepository.FindScheduleById(newTimesheet.ScheduleId);
            var timesheet = new TimesheetModel()
            {
                CreateTime = DateTime.UtcNow,
                DateFrom = newTimesheet.DateFrom,
                DateTo = newTimesheet.DateTo != null ? newTimesheet.DateTo : (DateTime?) null,
                Schedule = schedule,
                Time = newTimesheet.Time
            };

            _timesheetRepository.CreateTimesheet(timesheet);

            return Ok(newTimesheet);
        }

        public IHttpActionResult GetDeviceTimsheets(string deviceId)
        {
            var device = _deviceRepository.GetDeviceByDeviceId(deviceId);
            var schedule = _scheduleRepository.FindScheduleByDevice(device);

            var result = _timesheetRepository.GetTimesheets(schedule).Select(t => t.MapToDTO());

            return Ok(result);
        }
    }
}
