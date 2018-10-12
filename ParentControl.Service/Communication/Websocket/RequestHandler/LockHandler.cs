using Newtonsoft.Json;
using ParentControl.Service.Command;
using ParentControl.Service.Contracts;

namespace ParentControl.Service.Communication.Websocket.RequestHandler
{
    class LockHandler : IWebsocketRequestSolver
    {
        public string Command => "LOCK";

        private static CommandExecuter _commandExecuter;

        public LockHandler()
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
