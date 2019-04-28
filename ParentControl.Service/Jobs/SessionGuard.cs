using System;
using System.Threading;
using System.Threading.Tasks;
using ParentControl.Service.Command;
using ParentControl.Service.Consts;

namespace ParentControl.Service.Jobs
{
    class SessionGuard : BaseJob
    {
        private readonly int _syncTicksLimit;

        private Task _timer;
        private bool _stopFlag;
        private int _syncTicksCounter;

        private const string SyncTicksLimitSettingKey = "SessionGuard.SyncTicks";

        public override string ID => "session-guard";
        public override bool KeepAlive => true;

        public SessionGuard()
        {
            if (!int.TryParse(System.Configuration.ConfigurationSettings.AppSettings[SyncTicksLimitSettingKey], out _syncTicksLimit))
            {
                throw new InvalidOperationException($"Invalid {SyncTicksLimitSettingKey} in web.config");
            }
        }

        public override void Start()
        {
            _timer = Task.Run(() =>
            {
                while (_stopFlag == false)
                {
                    SaveSessionEnd();
                    CheckAndCloseSessionIfTimeFinished();
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

            _stopFlag = false;
            ChangeState(JobState.Running);
        }

        private void SaveSessionEnd()
        {
            if (_syncTicksCounter == _syncTicksLimit || _syncTicksCounter > _syncTicksLimit)
            {
                var session = Context.ParentControlService.SessionService.EndSession(Context.ActiveSession);
                Context.ParentControlService.LocalSessionTracker.SaveSession(session);
                _syncTicksCounter = 0;
            }
            else
            {
                _syncTicksCounter++;
            }
        }

        private void CheckAndCloseSessionIfTimeFinished()
        {
            if ((Context.TimeLeft == TimeSpan.Zero || Context.TimeLeft < TimeSpan.Zero) && !Context.Schedule.AllowWitoutTimesheet)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"SessionGuard: Time ended.");
                Console.ForegroundColor = ConsoleColor.White;
                _stopFlag = true;
                var commandExecuter = new CommandExecuter();
                commandExecuter.Execute("job stop timer");
                commandExecuter.Execute("shutdown");
            }
        }

        public override void Stop()
        {
            _stopFlag = true;
            ChangeState(JobState.Stopped);
        }
    }
}
