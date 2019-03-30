using System;
using System.Threading;
using Akka.Actor;
using SupervisorStrategyLab.Model.Messages;

namespace SupervisorStrategyLab.Actors
{
    public class ChildActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            if (message is string msg && msg.Length > 1)
            {
                Console.WriteLine($"{this.Self.Path}: {msg}");
            }
            else if (message is Retry retry)
            {
                this.OnReceive(retry.Message);
            }
            else
            {
                Thread.Sleep(3000);
                throw new ArgumentOutOfRangeException(nameof(message));
            }
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine($"{this.Self.Path}: " + reason.GetBaseException().Message);

            var retry = message is Retry oldRetry
                            ? new Retry(oldRetry.Message, oldRetry.Ttl - 1)
                            : new Retry(message, 3);

            if (retry.Ttl > 0) this.Self.Tell(retry, this.Sender);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            Console.WriteLine("Restarted");
        }
    }
}