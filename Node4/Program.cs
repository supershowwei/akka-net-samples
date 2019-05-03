using System;
using System.Threading;
using Akka.Actor;
using Akka.Cluster.Routing;
using Akka.Routing;
using Node4.Actors;

namespace Node4
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sys = ActorSystem.Create("cluster-sys");

            var hello = sys.ActorOf(
                Props.Create(() => new HelloActor())
                    .WithRouter(
                        new ClusterRouterPool(new RoundRobinPool(20), new ClusterRouterPoolSettings(10000, 10, true))),
                "hello");

            var index = 0;
            while (true)
            {
                Thread.Sleep(1000);
                hello.Tell(++index);
            }
        }
    }
}