namespace Shared.Model.Messages
{
    public sealed class ReliableDeliveryAck
    {
        public ReliableDeliveryAck(decimal deliveryId)
        {
            this.DeliveryId = deliveryId;
        }

        public decimal DeliveryId { get; }
    }
}