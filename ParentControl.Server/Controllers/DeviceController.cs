using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using ParentControl.DTO;
using ParentControl.Server.Contracts.Repositories;
using ParentControl.Server.DTO;
using ParentControl.Server.Exceptions;
using ParentControl.Server.Mappers;
using ParentControl.Server.Models;

namespace ParentControl.Server.Controllers
{
    [Authorize]
    public class DeviceController : BaseController
    {
        private IDeviceRepository _deviceRepository;
        private IScheduleRepository _scheduleRepository;

        public DeviceController(IDeviceRepository deviceRepository, IUserRepository userRepository, IScheduleRepository scheduleRepository) : base(userRepository)
        {
            _deviceRepository = deviceRepository;
            _scheduleRepository = scheduleRepository;
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult Register(Device newDevice)
        {
            try
            {
                var device = new DeviceModel()
                {
                    DeviceId = newDevice.DeviceId,
                    Name = newDevice.Name,
                    User = AppUser
                };

                _deviceRepository.RegisterDevice(device);
                _scheduleRepository.CreateSchedule("Schedule", device);

                return Ok(newDevice);
            }
            catch (DeviceAlreadyExists ex)
            {
                return BadRequest("Device already exists");
            }
        }
        [HttpGet]
        public IHttpActionResult GetDevices()
        {
            try
            {
                var devices = _deviceRepository.GetDevicesForUser(AppUser).Select(d => d.MapToDTO());
                return Ok(devices);
            }
            catch (DeviceAlreadyExists ex)
            {
                return BadRequest("Cannot retrieve devices");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}