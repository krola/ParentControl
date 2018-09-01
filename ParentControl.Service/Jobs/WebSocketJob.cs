using System;
using ParentControl.Service.Consts;

namespace ParentControl.Service.Jobs
{
    class WebSocketJob : IJob
    {
        private JobState _state = JobState.Stopped;
        private App Context = App.Context;

        public WebSocketJob()
        {
        }

        public string ID => "websocket";

        public bool KeepAlive => true;

        public JobState GetState()
        {
            return _state;
        }

        public void Start()
        {
            _state = JobState.Running;
        }

        public void Stop()
        {
            _state = JobState.Stopped;
        }
    }
}
