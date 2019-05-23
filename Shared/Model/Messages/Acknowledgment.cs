namespace Shared.Model.Messages
{
    public sealed class Acknowledgment
    {
        public Acknowledgment(decimal messageId)
        {
            this.MessageId = messageId;
        }

        public decimal MessageId { get; }
    }
}