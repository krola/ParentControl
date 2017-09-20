﻿using System;
using System.Collections.Generic;
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
                _owinHandler.PostRequest("/api/Session/Start", session);
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
                _owinHandler.PostRequest("/api/Session/End", session);
            }
            catch (Exception)
            {
                //log
            }

            return session;
        }

        public IEnumerable<Session> TodaySessions(string deviceId)
        {
            try
            {
                var result = _owinHandler.GetRequest("/api/Session/GetSessionsForDay", new RequestParameter[]
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
                _owinHandler.PostRequest("/api/Session/AddUpdateSession", session);
            }
            catch (Exception)
            {
                //log
            }


        }

        public SessionService(IOwinHandler owinHandler) : base(owinHandler)
        {

        }
    }
}