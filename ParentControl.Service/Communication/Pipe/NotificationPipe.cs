using Newtonsoft.Json;
using ParentControl.Infrastructure.Communication.NamedPipes;
using ParentControl.Infrastructure.Constants;
using System;
using System.IO.Pipes;
using System.Text;
using System.Threading;

namespace ParentControl.Service.Communication.Pipe
{
    class NotificationPipe
    {
        public static void Send(string message, NotificationType type){
            var namedPipeClient = new NamedPipeClientStream(".", "NotificationPipe", PipeDirection.InOut);
            var sent = false;

            while (!sent)
            {
                try
                {
                    if (!namedPipeClient.IsConnected)
                    {
                        namedPipeClient.Connect();
                        namedPipeClient.ReadMode = PipeTransmissionMode.Message;
                    }

                    var data = JsonConvert.SerializeObject(new NotificationPipeModel() { NotificationType = type, Text = message });
                    byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                    namedPipeClient.Write(dataBytes, 0, dataBytes.Length);
                    namedPipeClient.Flush();
                    sent = namedPipeClient.IsMessageComplete;
                    Thread.Sleep(50);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"NotificationPipe: {ex.Message}");
                    Console.ForegroundColor = ConsoleColor.White;
                    namedPipeClient.Close();
                    Thread.Sleep(500);
                }
            }

            //namedPipeClient.Clo();
            namedPipeClient.Dispose();
        }
    }
}
