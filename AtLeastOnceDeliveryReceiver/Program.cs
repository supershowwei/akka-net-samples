using System;
using Akka.Actor;
using AtLeastOnceDeliveryReceiver.Actors;

namespace AtLeastOnceDeliveryReceiver
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sys = ActorSystem.Create("sys");

            var actor = sys.ActorOf(Props.Create<ChatRoomActor>(), "chatroom");

            Console.ReadKey();

            actor.Tell(PoisonPill.Instance);

            Console.ReadKey();
        }
    }
}