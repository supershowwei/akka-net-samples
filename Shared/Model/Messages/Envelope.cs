namespace Shared.Model.Messages
{
    public sealed class Envelope<T>
    {
        public Envelope(decimal messageId, T message)
        {
            this.MessageId = messageId;
            this.Message = message;
        }

        public decimal MessageId { get; }

        public T Message { get; }
    }
}