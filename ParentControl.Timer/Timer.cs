using NamedPipeWrapper;
using Newtonsoft.Json;
using ParentControl.Infrastructure.Communication.NamedPipes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParentControl.Timer
{
    public partial class Timer : Form
    {
        NamedPipeClientStream _namedPipeClient;


        public Timer()
        {
            InitializeComponent();
            ShowInTaskbar = false;
            _namedPipeClient = new NamedPipeClientStream(".","TimerPipe", PipeDirection.InOut);
           
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    try
                    {
                        if (!_namedPipeClient.IsConnected)
                        {
                            _namedPipeClient.Connect();
                            _namedPipeClient.ReadMode = PipeTransmissionMode.Message;
                        }

                        StringBuilder messageBuilder = new StringBuilder();
                        string messageChunk = string.Empty;
                        byte[] messageBuffer = new byte[32];
                        do
                        {
                            _namedPipeClient.Read(messageBuffer, 0, messageBuffer.Length);
                            messageChunk = Encoding.UTF8.GetString(messageBuffer);
                            messageBuilder.Append(messageChunk);
                            messageBuffer = new byte[messageBuffer.Length];
                        }
                        while (!_namedPipeClient.IsMessageComplete);
                        var timerObject = JsonConvert.DeserializeObject<TimerPipeModel>(messageBuilder.ToString());
                        SetText(timerObject.TimeLeft.ToString(@"hh\:mm\:ss"));
                        Thread.Sleep(1000);
                    } catch (Exception ex)
                    {
                        _namedPipeClient.Close();
                        Thread.Sleep(5000);
                    }
                    
                }
            });
            //FixWindowsSize();
        }

        private void Timer_FormClosing(object sender, FormClosingEventArgs e)
        {
            _namedPipeClient.Close();
            _namedPipeClient.Dispose();
        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.lbTimer.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.lbTimer.Text = text;
            }
        }
    }
}
