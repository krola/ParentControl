using System;
using System.Web.Http;
using ParentControl.Server.Contracts.Repositories;
using ParentControl.Server.Mappers;

namespace ParentControl.Server.Controllers
{
    [Authorize]
    public class ScheduleController : BaseController
    {
        private IDeviceRepository _deviceRepository;
        private IScheduleRepository _scheduleRepository;
        public ScheduleController(IUserRepository userRepository, IDeviceRepository deviceRepository, IScheduleRepository scheduleRepository) : base(userRepository)
        {
            _deviceRepository = deviceRepository;
            _scheduleRepository = scheduleRepository;
        }

        [HttpGet]
        public IHttpActionResult GetSchedule(string deviceId)
        {
            var device = _deviceRepository.GetDeviceByDeviceId(deviceId);
            
            if (device == null)
                return BadRequest("No device found");

            try
            {
                var schedule = _scheduleRepository.FindScheduleByDevice(device);
                return Ok(schedule.MapToDTO());
            }
            catch (Exception ex)
            {
                return BadRequest("Error getting schedule");
            }
        }
    }
}
