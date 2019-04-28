using Newtonsoft.Json;
using ParentControl.Service.Command;
using ParentControl.Service.Contracts;

namespace ParentControl.Service.Communication.Websocket.RequestHandler
{
    class EndHandler : IWebsocketRequestSolver
    {
        public string Command => "END";

        private static CommandExecuter _commandExecuter;

        public EndHandler()
        {
            _commandExecuter = new CommandExecuter();
        }

        public string Handle(string payload)
        {
            _commandExecuter.Execute("close session");
            _commandExecuter.Execute("shutdown");
            return string.Empty;
        }
    }
}
