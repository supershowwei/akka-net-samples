using System;
using Akka.Actor;
using Akka.Configuration;
using DispatcherLab.Actors;
using DispatcherLab.Model.Messages;

namespace DispatcherLab
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //LoadConfigFromParsing();

            LoadConfigFromAppConfig();

            Console.ReadKey();
        }

        private static void LoadConfigFromParsing()
        {
            var config = ConfigurationFactory.ParseString(
                @"
my-dispatcher {
    type = Dispatcher
    throughput = 100
    throughput-deadline-time = 0ms
}");
            var system = ActorSystem.Create("mySys", config);

            var helloActor = system.ActorOf(HelloActor.Props().WithDispatcher("my-dispatcher"), "hello");

            helloActor.Tell(new Who("Johnny"));
        }

        private static void LoadConfigFromAppConfig()
        {
            var system = ActorSystem.Create("mySys");

            var helloActor = system.ActorOf(HelloActor.Props(), "hello");
            var helloActor1 = system.ActorOf(HelloActor.Props(), "hello1");

            helloActor.Tell(new Who("Johnny"));
        }
    }
}