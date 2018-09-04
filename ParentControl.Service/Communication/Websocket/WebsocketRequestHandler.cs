using Newtonsoft.Json;
using ParentControl.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ParentControl.Service.Communication.Websocket
{
    public class WebsocketRequestHandler : IWebsocketRequestHandler
    {
        private App Context = App.Context;
        private IList<IWebsocketRequestSolver> _requestHandlers;

        public WebsocketRequestHandler()
        {
            _requestHandlers = new List<IWebsocketRequestSolver>();

            foreach (Type type in Assembly.GetAssembly(typeof(IWebsocketRequestSolver)).GetTypes()
               .Where(myType => myType.IsClass && !myType.IsAbstract && myType.GetInterfaces().Any(i => i == typeof(IWebsocketRequestSolver))))
            {
                var requestHandler = (IWebsocketRequestSolver)Activator.CreateInstance(type);
                var duplicate = _requestHandlers.FirstOrDefault(r => r.Command.Equals(requestHandler.Command, StringComparison.InvariantCultureIgnoreCase));
                if (duplicate != null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"WebsocketRequestHandler: {duplicate.GetType().Name} has has the same command command {requestHandler.Command} like {type.Name}");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    _requestHandlers.Add(requestHandler);
                }
            }
        }

        public void Handle(string data)
        {
            var request = JsonConvert.DeserializeObject<ServerRequestPocket>(data);
            var handler = _requestHandlers.FirstOrDefault(r => r.Command.Equals(request.Command, StringComparison.InvariantCultureIgnoreCase));

            if(handler == null)
            {
                return;
            }

            var serialized = JsonConvert.SerializeObject(new ServerResposePocket() { Command = request.Command, Origin = request.Origin, Payload = handler.Handle() });
            Context.WebsocketHandler.Send(serialized);
        }
    }
}
