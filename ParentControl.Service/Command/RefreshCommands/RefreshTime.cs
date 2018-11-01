using Newtonsoft.Json;
using ParentControl.Service.Communication.Websocket;
using ParentControl.Service.Communication.Websocket.RequestHandler;
using ParentControl.Service.Factories;
using ParentControl.Service.Initializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Service.Command.RefreshCommands
{
    class RefreshTime : BaseCommand
    {
        public override string Command => "TIME";

        public override void PrintInfo()
        {
            Console.WriteLine("\t" + Command + " Refresh session time left.");
        }

        protected override void Do(string[] args)
        {
            var timeInit = InitializersFactory.CreateInitializer(nameof(CalculateTime));
            var timesheetInit = InitializersFactory.CreateInitializer(nameof(LoadTimesheet), timeInit);
            var scheduleInit = InitializersFactory.CreateInitializer(nameof(LoadSchedule), timesheetInit);
            
            scheduleInit.Run();
            Context.JobManager.Start("session-guard");
            Context.JobManager.Start("timer");

            var handler = new StatusHandler();
            var response = JsonConvert.SerializeObject(new ServerResposePocket() { Command = handler.Command, Origin = string.Empty, Payload = handler.Handle(null) });
            Context.WebsocketHandler.Send(response);
        }
    }
}
