using Newtonsoft.Json;
using ParentControl.Infrastructure.Communication.NamedPipes;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParentControl.Service.Command.NotifyCommands
{
    class CloseAction : BaseCommand
    {
        public CloseAction()
        {
        }

        public override string Command => "CLOSE";

        public override void PrintInfo()
        {
            Console.WriteLine("\t" + Command + " - Show close system notification board");
        }

        protected override void Do(string[] args)
        {
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

                    var data = JsonConvert.SerializeObject(new NotificationPipeModel() { NotificationType = Infrastructure.Constants.NotificationAnwser.Shutdown });
                    byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                    namedPipeClient.Write(dataBytes, 0, dataBytes.Length);
                    namedPipeClient.Flush();
                    sent = namedPipeClient.IsMessageComplete;
                    Thread.Sleep(500);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"NotificationPipe: {ex.Message}");
                    Console.ForegroundColor = ConsoleColor.White;
                    namedPipeClient.Close();
                    Thread.Sleep(5000);
                }
            }

            namedPipeClient.Close();
            namedPipeClient.Dispose();
        }
    }
}
