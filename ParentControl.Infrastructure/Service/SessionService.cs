                using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ParentControl.Infrastructure.Contracts.Services;
                using ParentControl.Infrastructure.Mappers;
                using ParentControl.Infrastructure.Service.Model;
using Session = ParentControl.DTO.Session;

namespace ParentControl.Infrastructure.Service
{
    internal class SessionService : BaseService, ISessionService
    {
        public Session StartSession(int deviceId)
        {
            var createSessionParams = new CreateSessionParams()
            {
                SessionStart = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                DeviceId = deviceId
            };
           
            try
            {
                HttpService.PostRequest("/api/Session", createSessionParams);
            }
            catch (Exception)
            {
                //log
            }

            return createSessionParams.MapToSession();
        }

        public Session EndSession(Session session)
        {
            var endDate = DateTime.UtcNow;
            var updateSessionParams = new UpdateSessionParams()
            {
                Id = session.Id,
                SessionStart = session.SessionStart,
                SessionEnd = endDate
            };

            try
            {
                HttpService.PutRequest($"/api/Session", updateSessionParams);
            }
            catch (Exception)
            {
                //log
            }

            return session;
        }

        public IEnumerable<Session> TodaySessions(int deviceId)
        {
            try
            {
                var result = HttpService.GetRequest("/api/Session/", new RequestParameter[]
                {
                    new RequestParameter
                    {
                        Key = "Date",
                        Value = DateTime.UtcNow.ToString()
                    },
                    new RequestParameter
                    {
                        Key = "DeviceId",
                        Value = deviceId.ToString()
                    },
                });

                return JsonConvert.DeserializeObject<IEnumerable<Session>>(result);
            }
            catch (Exception)
            {
                //log
            }

            return new List<Session>();
        }

        public void UpdateSession(Session session, int deviceId)
        {
            try
            {
                var parameteres = session.MapToUpdateParameters();
                HttpService.PutRequest("/api/Session", parameteres);
            }
            catch (Exception)
            {
                //log
            }


        }

        public SessionService(IHttpService httpService) : base(httpService)
        {

        }
    }
}
