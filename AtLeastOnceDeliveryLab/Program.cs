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
                TimeSpan.Zero,
                TimeSpan.FromMilliseconds(100),
                actor,
                new ChatMessage("Johnny", "Hello", null),
                ActorRefs.NoSender);

            Console.ReadKey();
        }
    }
}