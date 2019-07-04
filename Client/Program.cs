using System;
using System.Collections.Immutable;
using System.Threading;
using Akka.Actor;
using Akka.Cluster.Tools.Client;
using Shared.Model.Messages;

namespace Client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //SendToShard();
            //SendToCluster();
            Send();
        }

        private static void Send()
        {
            var sys = ActorSystem.Create("sys");

            var client = sys.ActorSelection("akka.tcp://wantgoo-quoter@localhost:9527/user/quote-provider");

            while (true)
            {
                client.Tell("軟體工程師是一種把咖啡因轉成程式碼的裝置，而StackOverflow能有效的加速進程。");
            }

            //var actor = client.ResolveOne(TimeSpan.FromSeconds(5)).Result;

            //((IInternalActorRef)actor).Stop();
            //((IInternalActorRef)actor).Start();

            Console.ReadKey();
        }

        private static void SendToShard()
        {
            var sys = ActorSystem.Create("sys");

            var client = sys.ActorSelection("akka.tcp://cluster-sys@localhost:2561/system/sharding/hello");

            var random = new Random(Guid.NewGuid().GetHashCode());

            do
            {
                //client.Tell(new SharedEnvelope(random.Next(100).ToString(), "Andy"));
                client.Tell(new SharedEnvelope("a", "Andy"));
                client.Tell(new SharedEnvelope("b", "Johnny"));
                client.Tell(new SharedEnvelope("c", "Mary"));

                Thread.Sleep(1000);
            }
            while (true);
        }

        private static void SendToCluster()
        {
            var sys = ActorSystem.Create("sys");

            var client = sys.ActorOf(
                ClusterClient.Props(
                    ClusterClientSettings.Create(sys)
                        .WithInitialContacts(
                            ImmutableHashSet.Create(
                                new[] { ActorPath.Parse("akka.tcp://cluster-sys@localhost:2551/system/receptionist") }))),
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