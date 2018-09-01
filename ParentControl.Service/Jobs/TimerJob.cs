using System;
using System.Threading;
using System.Threading.Tasks;
using NamedPipeWrapper;
using ParentControl.Infrastructure.Communication.NamedPipes;
using ParentControl.Service.Consts;

namespace ParentControl.Service.Jobs
{
    class TimerJob : IJob
    {
        private JobState _state = JobState.Stopped;
        private App Context = App.Context;
        private Task _timer;

        private bool stopFlag;

        public TimerJob()
        {
        }

        public string ID => "timer";

        public bool KeepAlive => false;

        public JobState GetState()
        {
            return _state;
        }

        public void Start()
        {
            stopFlag = false;
            _timer = Task.Run(() =>
            {
                while (Context.TimeLeft > TimeSpan.Zero && stopFlag == false)
                { 
                    Context.TimeLeft = Context.TimeLeft.Subtract(new TimeSpan(0, 0, 1));
                    Thread.Sleep(1000);
                }
            });

            _timer.GetAwaiter().OnCompleted(() => {
                _state = JobState.Stopped;
            });

            _state = JobState.Running;
        }

        public void Stop()
        {
            stopFlag = true;
            _state = JobState.Stopped;
        }
    }
}
