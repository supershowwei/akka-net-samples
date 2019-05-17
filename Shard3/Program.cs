using System.Threading;
using Akka.Actor;
using Akka.Cluster.Sharding;
using Shared;
using Shared.Actors;

namespace Shard3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sys = ActorSystem.Create("cluster-sys");

            var region = ClusterSharding.Get(sys)
                .Start(
                    typeName: "hello",
                    entityProps: Props.Create<HelloActor>(),
                    settings: ClusterShardingSettings.Create(sys),
                    messageExtractor: new MessageExtractor(100));

            var count = 1;

            while (true)
            {
                region.Tell(new SharedEnvelope(count.ToString(), "Bob"));

                count++;

                if (count > 100) count = 1;

                Thread.Sleep(100);
            }
        }
    }
}