﻿using Newtonsoft.Json;
using ParentControl.Service.Contracts;
using System;
using System.Linq;

namespace ParentControl.Service.Communication.Websocket.RequestHandler
{
    class StatusHandler : IWebsocketRequestSolver
    {
        public string Command => "STATUS";

        public string Handle(string payload)
        {
            var sessionTime = App.Context.ActiveSession != null ? DateTime.UtcNow.Subtract(App.Context.ActiveSession.SessionStart).TotalSeconds : 0;
            var resultPayload = new
            {
                Status = 1,
                App.Context.Locked,
                TimeSpendOnSessionInSeconds = sessionTime,
                TimeSpendTodayInSeconds = App.Context.TodaySessions.Where(s => s.SessionEnd != null).Sum(s => s.SessionEnd.Value.Subtract(s.SessionStart).TotalSeconds) + sessionTime,
                TimeLeftInSeconds = App.Context.TimeLeft.TotalSeconds
            };

            return JsonConvert.SerializeObject(resultPayload);
        }
    }
}
