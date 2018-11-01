using Newtonsoft.Json;
using ParentControl.Service.Command;
using ParentControl.Service.Contracts;

namespace ParentControl.Service.Communication.Websocket.RequestHandler
{
    class RefreshHandler : IWebsocketRequestSolver
    {
        public string Command => "refresh time";

        private static CommandExecuter _commandExecuter;

        public RefreshHandler()
        {
            _commandExecuter = new CommandExecuter();
        }

        public string Handle(string payload)
        {
            _commandExecuter.Execute("refresh time");
            return string.Empty;
        }
    }
}
