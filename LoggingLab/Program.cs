using System;
using Akka.Actor;
using LoggingLab.Actors;

namespace LoggingLab
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Log4NetConfig.Init();

            var system = ActorSystem.Create("sys");

            var actor = system.ActorOf(Props.Create(() => new TestActor()));

            actor.Tell(string.Empty);

            Console.ReadKey();
        }
    }
}