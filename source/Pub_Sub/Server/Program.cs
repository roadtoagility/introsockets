using System;
using Core;
using NetMQ;
using NetMQ.Sockets;

namespace Server
{
    
    
    class Program
    {
        static void Main(string[] args)
        {
            using (var pubSocket = new PublisherSocket("@tcp://*:11000"))
            using(var responseSub = new SubscriberSocket(">tcp://localhost:10000"))
            {
                responseSub.Subscribe("getHello");
                
                while (true)
                {
                    var topic = responseSub.ReceiveFrameString();
                    Console.WriteLine($"Topic: {topic}");
                    
                    var messageJson = responseSub.ReceiveFrameString();
                    var message = Newtonsoft.Json.JsonConvert.DeserializeObject<Message>(messageJson);

                    pubSocket.SendMoreFrame(message.Id).SendFrame($"Hello, {message.Name}");
                }
            }
        }
    }
}