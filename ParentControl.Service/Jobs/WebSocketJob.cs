using Newtonsoft.Json;
using ParentControl.Service.Communication.Websocket;
using ParentControl.Service.Communication.Websocket.RequestHandler;
using ParentControl.Service.Consts;

namespace ParentControl.Service.Jobs
{
    class WebSocketJob : BaseJob 
    {
        public override string ID => "websocket";
        public override bool KeepAlive => true;

        public WebSocketJob()
        {
            if(Context.Mode == Constants.Mode.Offline)
            {
                ChangeState(JobState.Disabled);
            }

            OnJobStarted += () => {
                var handler = new StatusHandler();
                var response = JsonConvert.SerializeObject(new ServerResposePocket() { Command = handler.Command, Origin = string.Empty, Payload = handler.Handle(null) });
                Context.WebsocketHandler.Send(response);
            };

            OnJobStopped += () => {
                var handler = new StatusHandler();
                var response = JsonConvert.SerializeObject(new ServerResposePocket() { Command = handler.Command, Origin = string.Empty, Payload = JsonConvert.SerializeObject(new { Status = 0 }) });
                Context.WebsocketHandler.Send(response);
            };
        }

        private void _websocketManager_Stopped()
        {
            ChangeState(JobState.Stopped, false);
        }

        private void _websocketManager_Running()
        {
            ChangeState(JobState.Running);
        }

        public override void Start()
        {
            Context.WebsocketHandler.OnConnected += _websocketManager_Running;
            Context.WebsocketHandler.OnError += _websocketManager_Stopped;
            Context.WebsocketHandler.OnDisconnected += _websocketManager_Stopped;
            Context.WebsocketHandler.Connect();
        }

        public override void Stop()
        {
            RaiseOnJobStopped();
            Context.WebsocketHandler.Disconnect();
        }
    }
}
