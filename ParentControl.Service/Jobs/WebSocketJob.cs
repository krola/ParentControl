using ParentControl.Service.Consts;
using ParentControl.Service.Manager;

namespace ParentControl.Service.Jobs
{
    class WebSocketJob : IJob
    {
        private JobState _state = JobState.Stopped;
        private App Context = App.Context;

        public WebSocketJob()
        {
            if(Context.Mode == Constants.Mode.Offline)
            {
                _state = JobState.Disabled;
            }
        }

        private void _websocketManager_Stopped()
        {
            _state = JobState.Stopped;
        }

        private void _websocketManager_Running()
        {
            _state = JobState.Running;
        }

        public string ID => "websocket";

        public bool KeepAlive => true;

        public JobState GetState()
        {
            return _state;
        }

        public void Start()
        {
            Context.WebsocketHandler.OnConnected += _websocketManager_Running;
            Context.WebsocketHandler.OnError += _websocketManager_Stopped;
            Context.WebsocketHandler.OnDisconnected += _websocketManager_Stopped;
            Context.WebsocketHandler.Connect();
        }

        public void Stop()
        {
            Context.WebsocketHandler.Disconnect();
        }
    }
}
