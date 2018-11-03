using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParentControl.Service.Consts;
using ParentControl.Service.Exceptions;

namespace ParentControl.Service.Jobs
{
    class TimeDashboardJob : BaseJob
    {
        private const string ApplicationPathConfigKey = "Application.Timer.Path";
        private const string AppName = "ParentControl.Timer";
        private Task<int> _dashboard;
        private Process _process;

        public TimeDashboardJob()
        {
        
        }

        public override string ID => "timer-dashboard";
        public override bool KeepAlive => true;

        public override void Start()
        {
            var applicationPath = System.Configuration.ConfigurationSettings.AppSettings[ApplicationPathConfigKey];
            ValidateApplication(applicationPath);

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
            _process.Refresh();
            if (!_process.HasExited)
            {
                _process.Kill();
            }
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

        private void ValidateApplication(string applicationPath)
        {
            if (string.IsNullOrEmpty(applicationPath))
            {
                throw new JobStartException("Timer application path is not configured.");
            }

            if (!File.Exists(applicationPath) && !FileVersionInfo.GetVersionInfo(applicationPath).ProductName.Equals(AppName))
            {
                throw new JobStartException("Wrong timer application");
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
