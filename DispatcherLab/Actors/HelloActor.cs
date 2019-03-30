using System;
using Akka.Actor;
using DispatcherLab.Model.Messages;

namespace DispatcherLab.Actors
{
    public class HelloActor : ReceiveActor
    {
        public HelloActor()
        {
            this.Receive<Who>(who => { Console.WriteLine($"{this.Self.Path}: Hello {who.Name}!!!"); });
        }

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new HelloActor());
        }
    }
}