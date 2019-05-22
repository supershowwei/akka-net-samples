using System;
using Akka.Actor;
using AtLeastOnceDeliveryLab.Actors;
using Shared.Model.Messages;

namespace AtLeastOnceDeliveryLab
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sys = ActorSystem.Create("sys");

            var actor = sys.ActorOf(Props.Create<AtLeastOnceActor>(), "atleastonce");

            sys.Scheduler.ScheduleTellRepeatedly(
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(1),
                actor,
                new ChatMessage("Johnny", "Hello"),
                ActorRefs.NoSender);

            Console.ReadKey();
        }
    }
}