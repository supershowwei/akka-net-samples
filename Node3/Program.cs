using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using Akka.Actor;
using Akka.Cluster.Tools.Client;

namespace Node3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sys = ActorSystem.Create("sys");

            var client = sys.ActorOf(
                ClusterClient.Props(
                    ClusterClientSettings.Create(sys)
                        .WithInitialContacts(
                            ImmutableHashSet.Create(
                                new[]
                                {
                                    ActorPath.Parse("akka.tcp://cluster-sys@localhost:2551/system/receptionist")
                                }))),
                "client");

            var random = new Random(Guid.NewGuid().GetHashCode());

            do
            {
                client.Tell(new ClusterClient.Send("/user/addition", random.Next(10000)));

                Thread.Sleep(1000);
            }
            while (true);
        }
    }
}