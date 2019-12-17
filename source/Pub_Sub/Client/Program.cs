using System;
using Core;
using NetMQ;
using NetMQ.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var pubSocket = new PublisherSocket("@tcp://*:10000"))
            using(var responseSub = new SubscriberSocket(">tcp://localhost:11000"))
            {
                while (true)
                {
                    Console.WriteLine("Please, enter your name");
                    var name = Console.ReadLine();

                    if (!string.IsNullOrEmpty(name))
                    {
                        var message = new Message(name, Guid.NewGuid().ToString());
                        pubSocket.SendMoreFrame("getHello").SendFrame(Newtonsoft.Json.JsonConvert.SerializeObject(message));
                        responseSub.Subscribe(message.Id);
                        var topic = responseSub.ReceiveFrameString();
                        var messageFromTopic = responseSub.ReceiveFrameString();
                        
                        Console.WriteLine($"Response: {messageFromTopic}");
                    }
                    
                }
            }
        }
    }
}