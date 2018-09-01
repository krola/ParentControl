using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;

namespace Parent_Control.Test
{
    class Program
    {
        private static string QueueName = "testQueue";
        private static QueueClient queueClient;

        static void Main(string[] args)
        {
            CreateQueue();
            ReceiveMessages();
            Console.ReadLine();
        }

        private static void CreateQueue()
        {
            NamespaceManager namespaceManager = NamespaceManager.Create();

            Console.WriteLine("\nCreating Queue '{0}'...", QueueName);

            // Delete if exists 
            if (namespaceManager.QueueExists(QueueName))
            {
                namespaceManager.DeleteQueue(QueueName);
            }

            namespaceManager.CreateQueue(QueueName);
        }

        private static void ReceiveMessages()
        {
            Console.WriteLine("\nReceiving message from Queue...");
            BrokeredMessage message = null;

            NamespaceManager namespaceManager = NamespaceManager.Create();
            queueClient = QueueClient.Create(QueueName);
            while (true)
            {
                try
                {
                    //receive messages from Queue 
                    message = queueClient.Receive(TimeSpan.FromSeconds(5));
                    if (message != null)
                    {
                        Console.WriteLine(string.Format("Message received: Id = {0}, Body = {1}", message.MessageId, message.GetBody<string>()));
                        // Further custom message processing could go here... 
                        message.Complete();
                    }
                    else
                    {
                        //no more messages in the queue 
                        break;
                    }
                }
                catch (MessagingException e)
                {
                    if (!e.IsTransient)
                    {
                        Console.WriteLine(e.Message);
                        throw;
                    }
                    else
                    {
                        HandleTransientErrors(e);
                    }
                }
            }
            queueClient.Close();
        }

        private static void HandleTransientErrors(MessagingException e)
        {
            //If transient error/exception, let's back-off for 2 seconds and retry 
            Console.WriteLine(e.Message);
            Console.WriteLine("Will retry sending the message in 2 seconds");
            Thread.Sleep(2000);
        }
    }
}
