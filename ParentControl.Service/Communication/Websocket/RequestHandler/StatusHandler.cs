using Newtonsoft.Json;
using ParentControl.Service.Contracts;
using System;
using System.Linq;

namespace ParentControl.Service.Communication.Websocket.RequestHandler
{
    class StatusHandler : IWebsocketRequestSolver
    {
        public string Command => "STATUS";

        public string Handle()
        {
            var sessionTime = DateTime.Now.Subtract(App.Context.ActiveSession.SessionStart).TotalSeconds;
            var payload = new
            {
                TimeSpendOnSessionInSeconds = sessionTime,
                TimeSpendTodayInSeconds = App.Context.TodaySessions.Where(s => s.SessionEnd != null).Sum(s => s.SessionEnd.Value.Subtract(s.SessionStart).TotalSeconds) + sessionTime,
                TimeLeftInSeconds = App.Context.TimeLeft.TotalSeconds
            };

            return JsonConvert.SerializeObject(payload);
        }
    }
}
