using System;
using System.Threading;
using System.Threading.Tasks;
using ParentControl.Service.Consts;

namespace ParentControl.Service.Jobs
{
    class TimerJob : BaseJob
    {
        private Task _timer;
        private bool stopFlag;

        public TimerJob()
        {
        }

        public override string ID => "timer";
        public override bool KeepAlive => false;

        public override void Start()
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
                if (Context.TimeLeft == TimeSpan.Zero)
                {
                    ChangeState(JobState.Finished);
                }
                else
                {
                    ChangeState(JobState.Stopped);
                }
            });

            ChangeState(JobState.Running);
        }

        public override void Stop()
        {
            stopFlag = true;
            ChangeState(JobState.Stopped);
        }
    }
}
