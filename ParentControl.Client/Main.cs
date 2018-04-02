using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ParentControl.DTO;
using ParentControl.Infrastructure.Mappers;
using ParentControl.Infrastructure.Service;

namespace ParentControl.Client
{
    enum LogType
    {
        Info,
        Error
    }

    enum Mode
    {
        Online,
        Offline
    }

    public partial class Main : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool LockWorkStation();

        private ParentControlService _parentControlService;
        private Schedule _schedule;
        private Device _device;
        private List<Session> _todaySessions;
        private Session _activeSession;
        private TimeSpan _timeLeft;
        private static Timer _timerUi;

        private bool _allowWithoutTimesheet;
        private Mode _mode;
        private Form _form;

        public Main()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            Initilize();
            ShowTimerForm();
            _timerUi.SetText("Loading");
            backgroundWorker1.RunWorkerAsync();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var Params = base.CreateParams;
                Params.ExStyle |= 0x80;
                return Params;
            }
        }

        private void Start()
        {
            try
            {
                if (_parentControlService.IsConnected == false && CanRunOffline() == false)
                {
                    throw new Exception($"Cannot get token!");
                }
                if (_parentControlService.IsConnected == false && CanRunOffline() == true)
                {
                    _mode = Mode.Offline;
                    Log($"Offline mode", LogType.Info);
                }
                if (_parentControlService.IsConnected == true)
                {
                    _mode = Mode.Online;
                    //Log($"Token: {_parentControlService.InfoData}", LogType.Info);
                }

                if (_mode == Mode.Online)
                {
                    LoadDevice();
                    LoadSchedule();
                    LoadTimesheet(); 
                    CleanSessions();
                    SyncSessions();
                    StartSession();
                    CalculateTime();
                    StartTimer();
                }
                else
                {
                    CleanSessions();
                    StartSession();
                    CalculateTime();
                    StartTimer();
                }
                
            }
            catch (Exception e)
            { 
                Log(e.Message, LogType.Error);
                _form.Opacity = 100;
            }
            
        }

        private bool CanRunOffline()
        {
            try
            {
                return _parentControlService.ConfigService.Config.Timesheets.Any();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void CleanSessions()
        {
            var weekBefore = DateTime.UtcNow.Subtract(new TimeSpan(7, 0, 0, 0));
            _parentControlService.LocalSessionTracker.Sessions.Where(d => d.SessionStart.Date < weekBefore.Date).ToList().ForEach(
                s =>
                {
                    _parentControlService.LocalSessionTracker.RemoveSession(s);
                });
        }

        private void Initilize()
        {
            try
            {
                _parentControlService = new ParentControlService();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadSchedule()
        {
            var scheduleService = _parentControlService.ScheduleService;
            try
            {

                _schedule = scheduleService.GetScheduleFor(_device.Id).FirstOrDefault();
                if (_schedule == null)
                {
                    throw new Exception("No schedule loaded");
                }

                Log($"Schedule loaded", LogType.Info);
            }
            catch (Exception e)
            {
                var message = $"Error {e.Message}";
                throw new Exception(message);
            }
        }

        private void LoadTimesheet()
        {
            var timesheetService = _parentControlService.TimesheetService;
            try
            {
                var timesheets = timesheetService.GetTimesheetFor(_schedule.Id).ToList();
                _parentControlService.ConfigService.SaveTimesheets(timesheets, _schedule.AllowWitoutTimesheet);
                Log($"Timesheet loaded", LogType.Info);
            }
            catch (Exception e)
            {
                var message = $"Error {e.Message}";
                throw new Exception(message);
            }
        }

        private void LoadDevice()
        {
            var deviceService = _parentControlService.DeviceService;
            try
            {
                var deviceId = _parentControlService.ConfigService.Config.Device?.Id;
                if (deviceId == null)
                {
                    throw new Exception("No device configured");
                }

                _device = deviceService.GetDevices()
                    .FirstOrDefault(d => d.Id == deviceId);
                if (_device == null)
                {
                    throw new Exception("No device loaded");
                }
                Log($"Device loaded: {_device.Id} -> {_device.Name}", LogType.Info);
            }
            catch (Exception e)
            {
                var message = $"Error {e.Message}";
                throw new Exception(message);
            }

        }

        private void SyncSessions()
        {
            var localSessions = _parentControlService.LocalSessionTracker.Sessions.Where(d => d.SessionStart.Date == DateTime.UtcNow.Date).ToList();
            var remoteSessions = _parentControlService.SessionService.TodaySessions(_device.Id);

            foreach (var remoteSession in remoteSessions)
            {
                _parentControlService.LocalSessionTracker.SaveSession(remoteSession);
            }

            foreach (var localSession in localSessions)
            {
                _parentControlService.SessionService.UpdateSession(localSession, _device.Id);
            }
        }

        private void StartSession()
        {
            _todaySessions = _parentControlService.LocalSessionTracker.Sessions?.Where(
                s => s.SessionStart.Date == DateTime.UtcNow.Date).ToList();
            if (_todaySessions == null || _todaySessions.All(s => s.SessionEnd != null))
            {
                if (_mode == Mode.Offline)
                {
                    _activeSession = new Session()
                    {
                        SessionStart = DateTime.UtcNow,
                        Id = Guid.NewGuid(),
                        Device = _parentControlService.ConfigService.Config.Device
                    };
                }
                else
                {
                    _activeSession = _parentControlService.SessionService.StartSession(_device.Id);
                }
            }
            else
            {
                _activeSession = _todaySessions.First(s => s.SessionEnd == null);
            }

            _parentControlService.LocalSessionTracker.SaveSession(_activeSession);
        }

        private void StartTimer()
        {
            if (_allowWithoutTimesheet == true)
            {
                return;
            }

            if (_timeLeft == TimeSpan.Zero || _timeLeft < TimeSpan.Zero)
            {
                StopSession();
                return;
            }


            Log($"Time left: {_timeLeft.ToString("h'h 'm'm 's's'")}", LogType.Info);

            while (true)
            {
                if (_timeLeft == TimeSpan.Zero || _timeLeft < TimeSpan.Zero)
                {
                    Log($"Session time ended", LogType.Info);
                    _timerUi?.CloseTimer();
                    StopSession();
                    break;
                    //LockWorkStation();
                }
                else
                {
                    if (_timeLeft == new TimeSpan(0, 5, 0))
                    {
                        backgroundWorker1.ReportProgress(1);
                    }
                    _timeLeft = _timeLeft.Subtract(new TimeSpan(0, 0, 0, 1));
                    _timerUi?.SetDateTime(_timeLeft);
                }
                Thread.Sleep(1000);
            }
        }

        private void CalculateTime()
        {
            var today = DateTime.UtcNow;
            var timesheet = _parentControlService.ConfigService.Config.Timesheets.FirstOrDefault(t => today > t.DateFrom && today < t.DateTo);
            if (timesheet == null)  
            {
                Log($"No timesheet for today!", LogType.Info);
                _allowWithoutTimesheet = _parentControlService.ConfigService.Config.AllowOnNoTimesheet;
            }
            else
            {
                Log($"Found timesheet. Total time: {timesheet.Time.ToString("h'h 'm'm 's's'")}", LogType.Info);
                //session 
                var allTimeSpendToday = _todaySessions != null ? new TimeSpan(_todaySessions.Where(s => s.SessionEnd != null).Sum(s => s.SessionEnd.Value.Subtract(s.SessionStart).Ticks)) : new TimeSpan();
                allTimeSpendToday = allTimeSpendToday.Add(today.Subtract(_activeSession.SessionStart));
                _timeLeft = timesheet.Time.Subtract(allTimeSpendToday);
            }
        }

        private void Log(string message, LogType logType)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate {
                    Log(message, logType);
                });
                return;
            }
            if (logType == LogType.Error)
            {
                rtLog.ForeColor = Color.Red;
            }
            else
            {
                rtLog.ForeColor = Color.Black;
            }
            rtLog.AppendText(message + Environment.NewLine);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopSession();
        }

        private void StopSession()
        {
            _activeSession = _parentControlService.SessionService.EndSession(_activeSession);
            _parentControlService.LocalSessionTracker.SaveSession(_activeSession);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Start();
        }

        private void ShowTimerForm()
        {
            _timerUi = new Timer();
            _timerUi.Show(this);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            _form = (Form)sender;
            _form.ShowInTaskbar = false;
            _form.Opacity = 0;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                MessageBox.Show("Zostało 5 min");
                var notify = new Notification(this);
                notify.Notify("Pozostało 5 minut!", Notification.Anwser.Ok);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var notify = new Notification(this);
            notify.Notify("Koniec czasu!", Notification.Anwser.Shutdown);
        }
    }
}
