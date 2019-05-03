using System;
using Akka.Actor;

namespace Node4.Actors
{
    public class HelloActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            Console.WriteLine($"{this.Self.Path}: Hello");
        }
    }
}