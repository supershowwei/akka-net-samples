using System;
using Akka.Actor;
using Akka.Cluster.Tools.Client;
using Node2.Actors;

namespace Node2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sys = ActorSystem.Create("cluster-sys");

            var subtration = sys.ActorOf(Props.Create(() => new SubtrationActor()), "subtration");

            ClusterClientReceptionist.Get(sys).RegisterService(subtration);

            Console.ReadKey();
        }
    }
}