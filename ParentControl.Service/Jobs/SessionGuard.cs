using System;
using System.Threading;
using System.Threading.Tasks;
using ParentControl.Service.Command;
using ParentControl.Service.Consts;

namespace ParentControl.Service.Jobs
{
    class SessionGuard : BaseJob
    {
        private Task _timer;
        private bool stopFlag;

        public override string ID => "session-guard";
        public override bool KeepAlive => true;

        public override void Start()
        {
            _timer = Task.Run(() =>
            {
                while (stopFlag == false)
                {
                    if(Context.TimeLeft == TimeSpan.Zero || Context.TimeLeft < TimeSpan.Zero)
                    {
                        var commandExecuter = new CommandExecuter();
                        commandExecuter.Execute("notify close");
                    }
                    Thread.Sleep(1000);
                }
            });

            _timer.GetAwaiter().OnCompleted(() => {
                if(Context.TimeLeft == TimeSpan.Zero)
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
