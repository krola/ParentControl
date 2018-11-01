using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO.Pipes;
using System.Threading;
using System.IO;
using ParentControl.Infrastructure.Constants;
using System.Text;
using Newtonsoft.Json;
using ParentControl.Infrastructure.Communication.NamedPipes;

namespace ParentControl.Notification
{
    public partial class Notification : Form
    {
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;
        private bool _userClicked;
        private NotificationType _type;
        private Task _task;
        private NamedPipeServerStream _server;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public Notification()
        {
            InitializeComponent();
            StartListing();
            HideWindow();
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

        public void Notify(string message, NotificationType anwser)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate {
                    Notify(message, anwser);
                });
                return;
            }

            _type = anwser;
           
            if(_type == NotificationType.Unlock)
            {
                HideWindow();
            }
            else
            {
                ShowWindow();
            }

            ShowMessage(message);
            ShowButton();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            switch (_type)
            {
                    case NotificationType.Ok:
                        _userClicked = true;
                       
                    break;
                    case NotificationType.Shutdown:
                        System.Diagnostics.Process.Start("shutdown", "/s /t 60");

                    break;
            }
            HideWindow();
        }

        private void Notification_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:
                    if (!_userClicked)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        e.Cancel = false;
                    }

                    break;
            }
        }

        private void ShowButton()
        {
            if(_type == NotificationType.Lock)
            {
                btnOK.Visible = false;
                return;
            }

            var pointX = (this.Bounds.Width / 2) - (btnOK.Width / 2);
            var pointY = (this.Bounds.Height - btnOK.Height) - 10;
            btnOK.Visible = true;
            btnOK.Location = new Point(pointX, pointY);
            btnOK.Text = _type == NotificationType.Ok ? "Ok" : "Wyłącz komputer";
        }

        private void ShowMessage(string message)
        {
            lbText.Text = message;

            var pointX = (this.Bounds.Width / 2) - (lbText.Width / 2);
            var pointY = (this.Bounds.Height / 2) - (lbText.Height / 2);

            lbText.Location = new Point(pointX, pointY);
        }

        private void ShowWindow()
        {
            Show();
            Opacity = 100;
            Visible = true;
        }

        private void HideWindow()
        {
            Hide();
            Opacity = 0;
        }

        private void Notification_Load(object sender, EventArgs e)
        {
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            TopMost = true;
            TopLevel = true;
            ShowInTaskbar = false;
        }

        private void StartListing()
        {
            _task = Task.Factory.StartNew(() =>
            {
                _server = new NamedPipeServerStream("NotificationPipe", PipeDirection.InOut, 1, PipeTransmissionMode.Message);
                while (true)
                {
                    try
                    {
                        if (!_server.IsConnected)
                        {
                            _server.WaitForConnection();
                        }

                        StringBuilder messageBuilder = new StringBuilder();
                        string messageChunk = string.Empty;
                        byte[] messageBuffer = new byte[64];
                        do
                        {
                            _server.Read(messageBuffer, 0, messageBuffer.Length);
                            messageChunk = Encoding.UTF8.GetString(messageBuffer);
                            messageBuilder.Append(messageChunk);
                            messageBuffer = new byte[messageBuffer.Length];
                        }
                        while (!_server.IsMessageComplete);
                        var notificationObject = JsonConvert.DeserializeObject<NotificationPipeModel>(messageBuilder.ToString());
                        if(notificationObject != null)
                        {
                            Notify(notificationObject.Text, notificationObject.NotificationType);
                        }
                    }
                    catch (IOException ex)
                    {
                        _server.Disconnect();
                    }

                    Thread.Sleep(100);
                }
            });
        }
    }
}
