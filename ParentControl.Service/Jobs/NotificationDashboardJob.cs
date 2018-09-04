using ParentControl.Service.Consts;
using ParentControl.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParentControl.Service.Jobs
{
    class NotificationDashboardJob : IJob
    {
        private JobState _state = JobState.Stopped;
        private App Context = App.Context;
        private Task<int> _dashboard;
        private Process _process;

        public NotificationDashboardJob()
        {
        }

        public string ID => "notification-dashboard";

        public bool KeepAlive => true;

        public JobState GetState()
        {
            return _state;
        }

        public void Start()
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
                _state = JobState.Stopped;
            });

            _state = JobState.Running;
        }

        public void Stop()
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
