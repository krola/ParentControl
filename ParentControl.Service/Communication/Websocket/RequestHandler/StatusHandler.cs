using Newtonsoft.Json;
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
            var sessionTime = DateTime.UtcNow.Subtract(App.Context.ActiveSession.SessionStart).TotalSeconds;
            var resultPayload = new
            {
                Status = 1,
                TimeSpendOnSessionInSeconds = sessionTime,
                TimeSpendTodayInSeconds = App.Context.TodaySessions.Where(s => s.SessionEnd != null).Sum(s => s.SessionEnd.Value.Subtract(s.SessionStart).TotalSeconds) + sessionTime,
                TimeLeftInSeconds = App.Context.TimeLeft.TotalSeconds
            };

            return JsonConvert.SerializeObject(resultPayload);
        }
    }
}
