namespace Shared.Model.Messages
{
    public sealed class SharedEnvelope
    {
        public SharedEnvelope(string entityId, object payload)
        {
            this.EntityId = entityId;
            this.Payload = payload;
        }

        public string EntityId { get; }

        public object Payload { get; }
    }
}