using System;
using Akka;
using Akka.Actor;
using SerializerLab2.Model.Messages;

namespace SerializerLab2.Actors
{
    internal class HelloActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            message.Match().With<Hello>(_ => Console.WriteLine(_.Message));
        }
    }
}