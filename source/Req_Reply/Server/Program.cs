using System;
using NetMQ;
using NetMQ.Sockets;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var replySocket = new ResponseSocket("@tcp://*:5555"))
            {
                while (true)
                {
                    var message = replySocket.ReceiveFrameString();
                    replySocket.SendFrame($"Olá, {message}");
                }
            }
        }
    }
}