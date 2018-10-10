using System;
using ParentControl.Service.Factories;
using ParentControl.Service.Initializers;
using ParentControl.Service.Manager;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ParentControl.Service
{
    class Core
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        private BaseInitializer _initializers;
        private static int _windowsStatus;

        public Core()
        {
            _windowsStatus = SW_SHOW;
        }

        public void Init()
        {
            _initializers = InitializersFactory.CreateProcessPipline();
        }

        public void Run()
        {
            try
            {
                BindHotkeys();
                _initializers.Run();
                if (App.Context.Initialized)
                {
                    App.Context.JobManager.Start();
                }
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
            }
        }

        private void BindHotkeys()
        {
            HotKeyManager.RegisterHotKey(Keys.F12, KeyModifiers.Windows);
            HotKeyManager.HotKeyPressed += new EventHandler<HotKeyEventArgs>(HotKeyManager_HotKeyPressed);
        }

        private static void HotKeyManager_HotKeyPressed(object sender, HotKeyEventArgs e)
        {
            var handle = GetConsoleWindow();
            if(_windowsStatus == SW_SHOW)
            {
                _windowsStatus = SW_HIDE;
            }
            else
            {
                _windowsStatus = SW_SHOW;
            }
            ShowWindow(handle, _windowsStatus);
        }
    }
}
