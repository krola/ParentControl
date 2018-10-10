using ParentControl.Service.Consts;
using ParentControl.Service.Exceptions;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ParentControl.Service.Jobs
{
    class NotificationDashboardJob : BaseJob
    {
        private Task<int> _dashboard;
        private Process _process;

        public NotificationDashboardJob()
        {
        }

        public override string ID => "notification-dashboard";
        public override bool KeepAlive => true;

        public override void Start()
        {
            var applicationPath = System.Configuration.ConfigurationSettings.AppSettings["Application.Notification.Path"];

            if (string.IsNullOrEmpty(applicationPath))
            {
                throw new JobStartException("Notification dashboard application path is not configured.");
            }

            if (!File.Exists(applicationPath) && !FileVersionInfo.GetVersionInfo(applicationPath).ProductName.Equals("ParentControl.Notification"))
            {
                throw new JobStartException("Wrong notification dashboard application");
            }

            _dashboard = RunProcessAsync(applicationPath);
            _dashboard.GetAwaiter().OnCompleted(() =>
            {
                ChangeState(JobState.Stopped);
            });

            ChangeState(JobState.Running);
        }

        public override void Stop()
        {
            _process.CloseMainWindow();
        }

        private Task<int> RunProcessAsync(string fileName)
        {
            var tcs = new TaskCompletionSource<int>();

            _process = new Process
            {
                StartInfo = { FileName = fileName },
                EnableRaisingEvents = true
            };

            _process.Exited += (sender, args) =>
            {
                tcs.SetResult(_process.ExitCode);
                _process.Dispose();
            };

            _process.Start();

            return tcs.Task;
        }
    }
}
