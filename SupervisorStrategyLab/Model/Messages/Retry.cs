namespace SupervisorStrategyLab.Model.Messages
{
    public sealed class Retry
    {
        public Retry(object message, int ttl)
        {
            this.Message = message;
            this.Ttl = ttl;
        }

        public object Message { get; }

        public int Ttl { get; } // Time to live
    }
}