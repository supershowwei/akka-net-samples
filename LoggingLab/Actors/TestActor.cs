using Akka.Actor;
using Akka.Event;

namespace LoggingLab.Actors
{
    internal class TestActor : UntypedActor
    {
        private static readonly ILoggingAdapter Log = Context.GetLogger();

        protected override void OnReceive(object message)
        {
            Log.Debug("Test debug");
            Log.Info("Test info");
            Log.Warning("Test warning");
            Log.Error("Test error");
        }
    }
}