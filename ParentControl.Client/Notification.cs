using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ParentControl.Client
{
    public partial class Notification : Form
    {
        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const UInt32 SWP_NOSIZE = 0x0001;
        private const UInt32 SWP_NOMOVE = 0x0002;
        private const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private readonly Form _owner;

        public enum Anwser
        {
            Ok,
            Shutdown
        }
        public Notification(Form owner)
        {
            _owner = owner;

            InitializeComponent();
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

        private bool _userClicked;
        private Anwser _anwser; 

        public void Notify(string message, Anwser anwser)
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
            label1.Text = message;

            var pointX = (this.Bounds.Width / 2) - (label1.Width / 2);
            var pointY = (this.Bounds.Height / 2) - (label1.Height / 2);

            label1.Location = new Point(pointX,pointY);

            pointX = (this.Bounds.Width / 2) - (btnOK.Width / 2);
            pointY = (this.Bounds.Height - btnOK.Height) - 10;
            btnOK.Location = new Point(pointX, pointY);
            btnOK.Text = anwser == Anwser.Ok ? "Ok" : "Wyłącz komputer";
            this.TopMost = true;
            this.TopLevel = true;
            this.ShowInTaskbar = false;
            this.ShowDialog(_owner);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            switch (_anwser)
            {
                    case Anwser.Ok:
                        _userClicked = true;
                        this.Close();
                    break;
                    case Anwser.Shutdown:
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
    }
}
