namespace ReceiveTimeoutLab.Messages
{
    public interface IEnvelope
    {
        ulong TrackingNo { get; }
    }
}