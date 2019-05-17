using Akka.Cluster.Sharding;

namespace Shared
{
    public sealed class MessageExtractor : HashCodeMessageExtractor
    {
        public MessageExtractor(int maxNumberOfShards)
            : base(maxNumberOfShards)
        {
        }

        public override string EntityId(object message) => (message as SharedEnvelope)?.EntityId;

        public override object EntityMessage(object message) => (message as SharedEnvelope)?.Payload;
    }
}