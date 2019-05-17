using System;
using Akka.Actor;

namespace Shared.Actors
{
    public class HelloActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            Console.WriteLine($"{this.Self.Path}: Hello, {message}");
        }
    }
}