using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using ParentControl.DTO;
using ParentControl.Infrastructure.Mappers;
using ParentControl.Infrastructure.Service;
using Timer = System.Timers.Timer;

namespace ParentControl.Service.App
{
    class Program
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool LockWorkStation();

        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        private static ParentControlService _parentControlService;
        private Timer _timeComponent;
        private Schedule _schedule;
        private Device _device;
        private List<Session> _todaySessions;
        private static Session _activeSession;
        private TimeSpan _timeLeft;

        private static Program _app;

        static void Main(string[] args)
        {
            var window = GetConsoleWindow();
            DeleteMenu(GetSystemMenu(window, false), SC_CLOSE, MF_BYCOMMAND);

            handler = new ConsoleEventDelegate(ConsoleEventCallback);
            SetConsoleCtrlHandler(handler, true);
            _app = new Program();
            _app.Initilize();
            _app.Start();
            //ShowWindow(window, SW_HIDE);
            Console.ReadLine();
        }

        private void Start()
        {
            if (_parentControlService.IsConnected == false)
            {
                Console.WriteLine($"Cannot get token: {_parentControlService.InfoData}");
                Console.ReadLine();
            }
            Console.WriteLine($"Token: {_parentControlService.InfoData}");
            LoadDevice();
            LoadSchedule();
            SyncSessions();
            StartSession();
            CalculateTime();
            StartTimer();
        }

        private void Initilize()
        {
            try
            {
                _parentControlService = new ParentControlService();
                
                _timeComponent = new Timer();
                _timeComponent.Interval = 1000; // 60 seconds  
                _timeComponent.Elapsed += this.tChecker_Tick;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void LoadSchedule()
        {
            var scheduleService = _parentControlService.ScheduleService;
            try
            {
                _schedule = scheduleService.GetDeviceSchedule(_device.DeviceId);
                if (_schedule == null)
                {
                    Console.WriteLine("No schedule loaded");
                    throw new Exception("No schedule loaded");
                }
                Console.WriteLine($"Schedule loaded");
            }
            catch (Exception e)
            {
                var message = $"Error {e.Message}";
                Console.WriteLine(message);
                throw e;
            }
        }

        private void LoadDevice()
        {
            var deviceService = _parentControlService.DeviceService;
            try
            {
                var deviceId = _parentControlService.ConfigService.Config.Device.DeviceId;
                _device = deviceService.GetDevices().FirstOrDefault(d => new Guid(d.DeviceId) == new Guid(deviceId));
                if (_device == null)
                {
                    Console.WriteLine("No device loaded");
                    throw new Exception("No device loaded");
                }
                Console.WriteLine($"Device loaded: {_device.DeviceId} -> {_device.Name}");
            }
            catch (Exception e)
            {
                var message = $"Error {e.Message}";
                Console.WriteLine(message);
                throw e;
            }

        }

        private void SyncSessions()
        {
            var localSessions = _parentControlService.ConfigService.Config.Sessions;
            var remoteSessions = _parentControlService.SessionService.TodaySessions(_device.DeviceId);

            foreach (var remoteSession in remoteSessions)
            {
                _parentControlService.ConfigService.SaveSession(remoteSession);
            }

            foreach (var localSession in localSessions)
            {
                _parentControlService.SessionService.UpdateSession(localSession.MapToSessionDTO(), _device.DeviceId);
            }
        }
        private void StartSession()
        {
            _todaySessions = _parentControlService.ConfigService.Config.Sessions?.Where(
                s => s.SessionStart.Date == DateTime.UtcNow.Date).Select(s => s.MapToSessionDTO()).ToList();
            if (_todaySessions == null || _todaySessions.All(s => s.SessionEnd != null))
            {
                _activeSession = _parentControlService.SessionService.StartSession(_device.DeviceId);
            }
            else
            {
                _activeSession = _todaySessions.First(s => s.SessionEnd == null);
            }

            _parentControlService.ConfigService.SaveSession(_activeSession);
        }

        private void StartTimer()
        {
            Console.WriteLine($"Time left: {_timeLeft.ToString("h'h 'm'm 's's'")}");
            _timeComponent.Start();
            //_server = new NamedPipeServerStream("ParentControl");
            //_server.WaitForConnection();
            //_writer = new StreamWriter(_server);
        }

        private void CalculateTime()
        {
            var today = DateTime.UtcNow;
            var timesheet = _schedule.Timesheets.FirstOrDefault(t => today > t.DateFrom && today < t.DateTo);
            if (timesheet == null)
            {
                Console.WriteLine($"No timesheet for today!");
                return;
            }
            Console.WriteLine($"Found timesheet. Total time: {timesheet.Time.ToString("h'h 'm'm 's's'")}");
            //session 
            var allTimeSpendToday = _todaySessions != null ? new TimeSpan(_todaySessions.Where(s => s.SessionEnd != null).Sum(s => s.SessionEnd.Value.Subtract(s.SessionStart).Ticks)) : new TimeSpan();
            allTimeSpendToday = allTimeSpendToday.Add(today.Subtract(_activeSession.SessionStart));
            _timeLeft = timesheet.Time.Subtract(allTimeSpendToday);
        }


        private void tChecker_Tick(object sender, ElapsedEventArgs e)
        {
            if (_timeLeft == TimeSpan.Zero || _timeLeft < TimeSpan.Zero)
            {
                Console.WriteLine($"Session time ended");
                LockWorkStation();
            }
            else
            {
                _timeLeft = _timeLeft.Subtract(new TimeSpan(0, 0, 0, 1));
            }
        }

        static bool ConsoleEventCallback(int eventType)
        {
            if (eventType == 2 || eventType == 1 || eventType == 0 || eventType == 5 || eventType == 6)
            {
                MessageBox.Show("Session ended");
                _activeSession = _parentControlService.SessionService.EndSession(_activeSession);
                _parentControlService.ConfigService.SaveSession(_activeSession);
            }
            return false;
        }
        static ConsoleEventDelegate handler;   // Keeps it from getting garbage collected
        // Pinvoke
        private delegate bool ConsoleEventDelegate(int eventType);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);
    }
}
