using System;
using ParentControl.Service.Consts;

namespace ParentControl.Service.Jobs
{
    public delegate void OnJobStarted();

    abstract class BaseJob : IJob
    {
        private JobState _state = JobState.Stopped;

        public abstract string ID { get; }
        public abstract bool KeepAlive { get; }

        public event OnJobStarted OnJobStarted;

        protected App Context = App.Context;

        public JobState GetState()
        {
            return _state;
        }

        protected void ChangeState(JobState newState)
        {
            _state = newState;
            if (_state == JobState.Running && OnJobStarted != null)
            {
                OnJobStarted();
            }
        }

        public abstract void Start();
        public abstract void Stop();
    }
}
