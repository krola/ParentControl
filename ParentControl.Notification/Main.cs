using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParentControl.Notification
{
    public partial class Main : Form
    {
        private NamedPipeClientStream _client;
        private StreamReader _reader;
        public Main()
        {
            
            InitializeComponent();
            initializer.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var line = _reader.ReadLine();
            lbText.Text = line;
        }

        private void initializer_DoWork(object sender, DoWorkEventArgs e)
        {
            if (initializer.CancellationPending == true)
            {
                e.Cancel = true;
                return; // this will fall to the finally and close everything    
            }

            _client = new NamedPipeClientStream("ParentControl");
            _client.Connect();
            _reader = new StreamReader(_client);

            refresher.RunWorkerAsync();
            initializer.CancelAsync();

        }
    }
}
