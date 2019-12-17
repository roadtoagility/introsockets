using System;
using NetMQ;
using NetMQ.Sockets;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var reqSocket = new RequestSocket(">tcp://localhost:5555"))
            {
                Console.WriteLine("Please, enter your name");
                
                while (true)
                {
                    var name = Console.ReadLine();

                    if (!string.IsNullOrEmpty(name))
                    {
                        reqSocket.SendFrame(name);
                        var response = reqSocket.ReceiveFrameString();
                        Console.WriteLine($"Response from server: {response}");
                    }
                    else
                    {
                        Console.WriteLine("Please, enter your name");
                    }
                }
            }
        }
    }
}