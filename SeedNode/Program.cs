using System;
using Akka.Actor;
using Akka.Routing;

namespace SeedNode
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sys = ActorSystem.Create("cluster-sys");

            Console.ReadKey();
        }
    }
}