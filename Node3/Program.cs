using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
                                    ActorPath.Parse("akka.tcp://cluster-sys@localhost:2553/system/receptionist")
                                }))),
                "client");

            do
            {
                client.Tell(new ClusterClient.Send("/user/addition", "111"));
            }
            while (Console.ReadLine() == string.Empty);
        }
    }
}