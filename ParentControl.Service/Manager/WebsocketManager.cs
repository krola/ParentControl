using ParentControl.Service.Command;
using ParentControl.Service.Communication.Websocket;
using ParentControl.Service.Contracts;
using ParentControl.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace ParentControl.Service.Manager
{
    class WebsocketHandler
    {
        private WebSocket _webSocket;
        private App Context = App.Context;

        IWebsocketRequestHandler _websocketRequestHandler;

        public WebsocketHandler()
        {
            _websocketRequestHandler = new WebsocketRequestHandler();
        }

        public bool Status => (_webSocket?.ReadyState ?? WebSocketState.Closed) == WebSocketState.Open ? true : false;

        public delegate void Connected();
        public delegate void Disconnected();
        public delegate void Error();
        public event Connected OnConnected;
        public event Disconnected OnDisconnected;
        public event Error OnError;

        public void Initialize()
        {
            var host = System.Configuration.ConfigurationSettings.AppSettings["Websocket.Host"];
            if(string.IsNullOrEmpty(host))
            {
                throw new MissingConfigurationException("Missing Websocket.Host configuration");
            }

            _webSocket = new WebSocket($"ws://{host}/sync?deviceid={Context.Device.Id}&type=1");
            _webSocket.OnOpen += (sender, e) => {
                Console.WriteLine("Websocket connection estalished.");
                OnConnected?.Invoke();
            };
            _webSocket.OnMessage += (sender, e) => {
                _websocketRequestHandler.Handle(e.Data);
            };
            _webSocket.OnClose += (sender, e) => {
                Console.WriteLine("Websocket connection closed.");
                OnDisconnected?.Invoke();
            };
            _webSocket.OnError += (sender, e) => {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"websocket: {e.Message}");
                Console.ForegroundColor = ConsoleColor.White;
                OnError?.Invoke();
            };
        }

        public void Connect()
        {
            _webSocket.Connect();
        }

        public void Disconnect()
        {
            _webSocket.Close();
        }

        public void Send(string data)
        {
            if (!Status)
            {
                return;
            }

            _webSocket.Send(data);
        }
    }
}
