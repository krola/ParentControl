using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ParentControl.Infrastructure.Communication.NamedPipes;
using ParentControl.Service.Consts;

namespace ParentControl.Service.Jobs
{
    class TimerNotificationJob : BaseJob
    {
        private Task _timer;
        private bool stopFlag;

        public override string ID => "timer-notification";

        public override bool KeepAlive => true;

        public override void Start()
        {
            stopFlag = false;
            _timer = Task.Factory.StartNew(() =>
            {
                var server = new NamedPipeServerStream("TimerPipe", PipeDirection.InOut, 1, PipeTransmissionMode.Message);
                while (stopFlag == false)
                {
                    try
                    {
                        if (!server.IsConnected)
                        {
                            server.WaitForConnection();
                        }
                    
                        var data = JsonConvert.SerializeObject(new TimerPipeModel() { TimeLeft = Context.TimeLeft });
                        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                        server.Write(dataBytes, 0, dataBytes.Length);
                        server.Flush();
                    }
                    catch(IOException ex)
                    {
                        server.Disconnect();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"TimerPipe: {ex.Message}");
                        Console.ForegroundColor = ConsoleColor.White;
                        Thread.Sleep(2000);
                    }
                    
                    Thread.Sleep(1000);
                }
                server.Close();
                server.Dispose();
            });

            _timer.GetAwaiter().OnCompleted(() => {
                ChangeState(JobState.Stopped);
            });

            ChangeState(JobState.Running);
        }

        public override void Stop()
        {
            stopFlag = true;
        }
    }
}
