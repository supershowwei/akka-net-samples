using System;
using Akka.Actor;
using PullWorker.Actors;

namespace PullWorker
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sys = ActorSystem.Create("sys");

            sys.ActorOf(Props.Create<ChatRoomActor>(), "chatroom");

            Console.ReadKey();
        }
    }
}