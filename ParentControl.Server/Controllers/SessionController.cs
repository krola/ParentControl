using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ParentControl.DTO;
using ParentControl.Server.Contracts.Repositories;
using ParentControl.Server.DTO;
using ParentControl.Server.Mappers;
using ParentControl.Server.Models;

namespace ParentControl.Server.Controllers
{
    public class SessionController : BaseController
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IDeviceRepository _deviceRepository;

        public SessionController(ISessionRepository sessionRepository, IDeviceRepository deviceRepository, IUserRepository userRepository) : base(userRepository)
        {
            _sessionRepository = sessionRepository;
            _deviceRepository = deviceRepository;
        }

        public IHttpActionResult GetSessionsForDay(DateTime day, string deviceId)
        {
            var device = _deviceRepository.GetDeviceByDeviceId(deviceId);
            var result = _sessionRepository.GetSessionForDate(day, device).Select(s => s.MapToDTO());

            return Ok(result);
        }

        public IHttpActionResult Start([FromBody]Session session)
        {
            var device = _deviceRepository.GetDeviceByDeviceId(session.DeviceID);
            _sessionRepository.AddSession(new SessionModel()
            {
                UniqueId = session.SessionId,
                StarTime = session.SessionStart,
                Device = device
            });
            return Ok();
        }

        public IHttpActionResult End([FromBody]Session session)
        {
            _sessionRepository.EndSession(session.SessionId, session.SessionEnd);
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult AddUpdateSession([FromBody]Session session)
        {
            var device = _deviceRepository.GetDeviceByDeviceId(session.DeviceID);
            var sessionModel = _sessionRepository.GetSessionByID(session.SessionId);
            if (sessionModel == null)
            {
                sessionModel = new SessionModel()
                {
                    Device = device,
                    UniqueId = session.SessionId,
                    StarTime = session.SessionStart
                };
                if (default(DateTime) != session.SessionEnd)
                {
                    sessionModel.EndTime = session.SessionEnd;
                }
                _sessionRepository.AddSession(sessionModel);
            }
            else
            {
                sessionModel.StarTime = session.SessionStart;
                sessionModel.EndTime = session.SessionEnd;
                _sessionRepository.Update();
            }
            
            return Ok();
        }
    }
}