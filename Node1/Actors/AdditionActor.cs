using System;
using Akka.Actor;

namespace Node1.Actors
{
    public class AdditionActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            Console.WriteLine($"{this.Self.Path}: {message}");
        }
    }
}