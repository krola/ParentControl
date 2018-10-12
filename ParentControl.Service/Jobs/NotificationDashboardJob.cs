using ParentControl.Service.Consts;
using ParentControl.Service.Exceptions;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ParentControl.Service.Jobs
{
    class NotificationDashboardJob : BaseJob
    {
        private const string AppName = "ParentControl.Notification";
        private const string ApplicationPathConfigKey = "Application.Notification.Path";

        private Task<int> _dashboard;
        private Process _process;

        public NotificationDashboardJob()
        {

        }

        public override string ID => "notification-dashboard";
        public override bool KeepAlive => true;

        public override void Start()
        {
            var applicationPath = System.Configuration.ConfigurationSettings.AppSettings[ApplicationPathConfigKey];
            ValidateApplicationPath(applicationPath);

            Process[] pnames = Process.GetProcessesByName(AppName);
            CheckProcesses(pnames);

            if (pnames.Any())
            {
                _process = pnames.First();
                Task.Run(() => _process.WaitForExit()).ContinueWith(t => ChangeState(JobState.Stopped));
            }
            else
            {
                _dashboard = RunProcessAsync(applicationPath);
                _dashboard.GetAwaiter().OnCompleted(() =>
                {
                    ChangeState(JobState.Stopped);
                });
            }

            ChangeState(JobState.Running);
        }

        public override void Stop()
        {
            _process.Kill();
        }

        private void CheckProcesses(Process[] pnames)
        {
            if (pnames.Length > 1)
            {
                System.Console.ForegroundColor = System.ConsoleColor.Yellow;
                System.Console.WriteLine($"NotificationDashboardJob: There are {pnames.Length} processes running. Please clean up.");
                System.Console.ForegroundColor = System.ConsoleColor.White;
            }
        }

        private void ValidateApplicationPath(string applicationPath)
        {
            if (string.IsNullOrEmpty(applicationPath))
            {
                throw new JobStartException("Notification dashboard application path is not configured.");
            }

            if (!File.Exists(applicationPath) && !FileVersionInfo.GetVersionInfo(applicationPath).ProductName.Equals(AppName))
            {
                throw new JobStartException("Wrong notification dashboard application");
            }
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
