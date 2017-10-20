using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParentControl.Core.Contracts;
using ParentControl.Core.Contracts.Services;
using ParentControl.Core.Services.Model;
using Session = ParentControl.DTO.Session;

namespace ParentControl.Core.Service
{
    public class SessionService : BaseService, ISessionService
    {
        public Session StartSession(string deviceId)
        {
            var session = new Session()
            {
                DeviceID = deviceId,
                SessionStart = DateTime.UtcNow,
                SessionId = Guid.NewGuid()
            };
           
            try
            {
                HttpService.PostRequestAsync("/api/Session/Start", session);
            }
            catch (Exception)
            {
                //log
            }

            return session;
        }

        public Session EndSession(Session session)
        {
            var endDate = DateTime.UtcNow;
            session.SessionEnd = endDate;
            try
            {
                HttpService.PostRequestAsync("/api/Session/End", session);
            }
            catch (Exception)
            {
                //log
            }

            return session;
        }

        public async Task<IEnumerable<Session>> TodaySessionsAsync(string deviceId)
        {
            try
            {
                var result = await HttpService.GetRequestAsync("/api/Session/GetSessionsForDay", new RequestParameter[]
                {
                    new RequestParameter
                    {
                        Key = "day",
                        Value = DateTime.UtcNow.ToString()
                    },
                    new RequestParameter
                    {
                        Key = "deviceId",
                        Value = deviceId
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

        public void UpdateSession(Session session, string deviceId)
        {
            try
            {
                session.DeviceID = deviceId;
                HttpService.PostRequestAsync("/api/Session/AddUpdateSession", session);
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
