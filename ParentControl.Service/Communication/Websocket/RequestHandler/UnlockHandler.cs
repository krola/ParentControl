using Newtonsoft.Json;
using ParentControl.Service.Command;
using ParentControl.Service.Contracts;

namespace ParentControl.Service.Communication.Websocket.RequestHandler
{
    class UnlockHandler : IWebsocketRequestSolver
    {
        public string Command => "UNLOCK";

        private static CommandExecuter _commandExecuter;

        public UnlockHandler()
        {
            _commandExecuter = new CommandExecuter();
        }

        public string Handle(string payload)
        {
            _commandExecuter.Execute(Command);
            var handler = new StatusHandler();
            return handler.Handle(payload);
        }
    }
}
