namespace ReceiveTimeoutLab.Messages
{
    public sealed class UrMessage : IEnvelope
    {
        public UrMessage(string id, ulong trackingNo)
        {
            this.Id = id;
            this.TrackingNo = trackingNo;
        }

        public string Id { get; }

        public ulong TrackingNo { get; }
    }
}