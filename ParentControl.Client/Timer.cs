using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParentControl.Client
{
    public partial class Timer : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
        public Timer()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
        }

        public void SetDateTime(TimeSpan time)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate {
                    SetDateTime(time);
                });
                return;
            }
            lbTimer.Text = time.ToString("h'h 'm'm 's's'");
            this.Width = lbTimer.Width + 20;
        }

        public void CloseTimer()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate {
                    CloseTimer();
                });
                return;
            }
            this.Close();
        }

        public void SetText(string text)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate {
                    SetText(text);
                });
                return;
            }
            lbTimer.Text = text;
        }
    }
}
