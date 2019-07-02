using System;
using Akka;
using Akka.Actor;

namespace ReceiveTimeoutLab.Actors
{
    public class HelloActor : UntypedActor
    {
        public HelloActor()
        {
            this.SetReceiveTimeout(TimeSpan.FromSeconds(5));
        }

        protected override void OnReceive(object message)
        {
            message.Match()
                .With<ReceiveTimeout>(_ => { Console.WriteLine("Timeout"); })
                .With<object>(_ => { Console.WriteLine("receive message"); });
        }
    }
}