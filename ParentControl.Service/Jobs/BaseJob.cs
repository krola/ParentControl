using System;
using ParentControl.Service.Consts;

namespace ParentControl.Service.Jobs
{
    public delegate void OnJobStarted();
    public delegate void OnJobStopped();

    abstract class BaseJob : IJob
    {
        private JobState _state = JobState.Stopped;

        public abstract string ID { get; }
        public abstract bool KeepAlive { get; }

        public event OnJobStarted OnJobStarted;
        public event OnJobStopped OnJobStopped;

        protected App Context = App.Context;

        public JobState GetState()
        {
            return _state;
        }

        protected void ChangeState(JobState newState, bool propageteEvents = true)
        {
            _state = newState;

            if (!propageteEvents)
            {
                return;
            }

            if (_state == JobState.Running)
            {
                OnJobStarted?.Invoke();
            }

            if (_state == JobState.Stopped)
            {
                RaiseOnJobStopped();
            }
        }

        protected void RaiseOnJobStopped()
        {
            OnJobStopped?.Invoke();
        }

        public abstract void Start();
        public abstract void Stop();
    }
}
