using System;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Configuration;
using Akka.Routing;
using RouterLab.Actors;
using RouterLab.Model.Messages;

namespace RouterLab
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //RoundRobinBroadcast();

            //UseResizer();

            RoundRobinGroup();
        }

        private static void RoundRobinBroadcast()
        {
            var system = ActorSystem.Create("mySys");

            var router = system.ActorOf(HelloActor.Props().WithRouter(FromConfig.Instance), "hello");

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                router.Tell(new Who("Johnny"));
            }
        }

        private static void UseResizer()
        {
            var system = ActorSystem.Create("mySys");

            var router = system.ActorOf(HelloActor.Props().WithRouter(FromConfig.Instance), "hello");

            var random = new Random(Guid.NewGuid().GetHashCode());

            while (true)
            {
                Parallel.For(0, random.Next(1000), i => { router.Tell(new Who("Johnny")); });

                System.Threading.Thread.Sleep(random.Next(10000));

                Console.Clear();
            }
        }

        private static void RoundRobinGroup()
        {
            var system = ActorSystem.Create("mySys");

            system.ActorOf(HelloActor.Props(), "hello");
            system.ActorOf(HelloActor.Props(), "hello1");

            var workers = new[] { "/user/hello", "/user/hello1" };

            var router = system.ActorOf(Props.Empty.WithRouter(new RoundRobinGroup(workers)), "hello-group");

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                router.Tell(new Who("Johnny"));
            }
        }
    }
}