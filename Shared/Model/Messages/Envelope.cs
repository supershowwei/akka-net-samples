namespace Shared.Model.Messages
{
    public sealed class Envelope<TMessage>
    {
        public Envelope(decimal deliveryId, TMessage message)
        {
            this.DeliveryId = deliveryId;
            this.Message = message;
        }

        public decimal DeliveryId { get; }

        public TMessage Message { get; }
    }

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