namespace Shared.Model.Messages
{
    public sealed class ReliableDeliveryEnvelope<T>
    {
        public ReliableDeliveryEnvelope(decimal deliveryId, T message)
        {
            this.DeliveryId = deliveryId;
            this.Message = message;
        }

        public decimal DeliveryId { get; }

        public T Message { get; }
    }
}