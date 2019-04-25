using System;
using Akka.Actor;

namespace Node2.Actors
{
    public class SubtrationActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            Console.WriteLine($"{this.Self.Path}: {message}");
        }
    }
}