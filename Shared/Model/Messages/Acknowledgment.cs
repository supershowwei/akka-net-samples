namespace Shared.Model.Messages
{
    public sealed class Acknowledgment
    {
        public Acknowledgment(decimal deliveryId)
        {
            this.DeliveryId = deliveryId;
        }

        public decimal DeliveryId { get; }
    }
}