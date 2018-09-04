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
        private NotificationAnwser _anwser;
        private Task _task;
        private NamedPipeServerStream _server;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public Notification()
        {
            InitializeComponent();
            StartListing();
            ShowInTaskbar = false;
            Hide();
            Opacity = 0;
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

        public void Notify(string message, NotificationAnwser anwser)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate {
                    Notify(message, anwser);
                });
                return;
            }
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Bounds = Screen.PrimaryScreen.Bounds;
            _anwser = anwser;
            lbText.Text = message;

            var pointX = (this.Bounds.Width / 2) - (lbText.Width / 2);
            var pointY = (this.Bounds.Height / 2) - (lbText.Height / 2);

            lbText.Location = new Point(pointX,pointY);

            pointX = (this.Bounds.Width / 2) - (btnOK.Width / 2);
            pointY = (this.Bounds.Height - btnOK.Height) - 10;
            btnOK.Location = new Point(pointX, pointY);
            btnOK.Text = anwser == NotificationAnwser.Ok ? "Ok" : "Wyłącz komputer";
            TopMost = true;
            TopLevel = true;
            Opacity = 100;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            switch (_anwser)
            {
                    case NotificationAnwser.Ok:
                        _userClicked = true;
                        Opacity = 0;
                    break;
                    case NotificationAnwser.Shutdown:
                        System.Diagnostics.Process.Start("shutdown", "/s /t 0");
                    break;
            }
            
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

        private void Notification_Load(object sender, EventArgs e)
        {
            SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
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
                        Thread.Sleep(2000);
                    }

                    Thread.Sleep(1000);
                }
            });
        }
    }
}
