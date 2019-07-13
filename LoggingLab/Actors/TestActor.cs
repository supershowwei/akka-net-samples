using Akka.Actor;
using Akka.Event;

namespace LoggingLab.Actors
{
    internal class TestActor : UntypedActor
    {
        private static readonly ILoggingAdapter Log = Context.GetLogger();
        private readonly string abc;

        public TestActor()
        {
            this.abc = "abc";
        }

        protected override void OnReceive(object message)
        {
            var a = 1;
            var b = 0;
            var c = a / b;

            Log.Debug("Test debug");
            Log.Info("Test info");
            Log.Warning("Test warning");
            Log.Error("Test error");
        }
    }
}