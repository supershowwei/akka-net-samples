using System;
using Akka.Actor;
using Akka.Cluster.Tools.Client;
using Node1.Actors;

namespace Node1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sys = ActorSystem.Create("cluster-sys");

            var addition = sys.ActorOf(Props.Create(() => new AdditionActor()), "addition");

            ClusterClientReceptionist.Get(sys).RegisterService(addition);

            Console.ReadKey();
        }
    }
}