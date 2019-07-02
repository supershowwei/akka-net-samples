namespace ReceiveTimeoutLab.Messages
{
    public sealed class ImMessage : IEnvelope
    {
        public ImMessage(string name, ulong trackingNo)
        {
            this.Name = name;
            this.TrackingNo = trackingNo;
        }

        public string Name { get; }

        public ulong TrackingNo { get; }
    }
}