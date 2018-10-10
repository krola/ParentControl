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
        private Task<int> _dashboard;
        private Process _process;

        public TimeDashboardJob()
        {
        }

        public override string ID => "timer-dashboard";
        public override bool KeepAlive => true;

        public override void Start()
        {
            var applicationPath = System.Configuration.ConfigurationSettings.AppSettings["Application.Timer.Path"];

            if (string.IsNullOrEmpty(applicationPath))
            {
                throw new JobStartException("Timer application path is not configured.");
            }

            if (!File.Exists(applicationPath) && !FileVersionInfo.GetVersionInfo(applicationPath).ProductName.Equals("ParentControl.Timer"))
            {
                throw new JobStartException("Wrong timer application");
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
