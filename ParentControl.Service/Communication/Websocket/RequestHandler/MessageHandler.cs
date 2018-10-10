using Newtonsoft.Json;
using ParentControl.Service.Command;
using ParentControl.Service.Contracts;

namespace ParentControl.Service.Communication.Websocket.RequestHandler
{
    class MessageHandler : IWebsocketRequestSolver
    {
        public string Command => "MESSAGE";

        private static CommandExecuter _commandExecuter;

        public MessageHandler()
        {
            _commandExecuter = new CommandExecuter();
        }

        public string Handle(string payload)
        {
            _commandExecuter.Execute($"notify text {payload}");
            return JsonConvert.SerializeObject(new { Message = "OK" });
        }
    }
}
