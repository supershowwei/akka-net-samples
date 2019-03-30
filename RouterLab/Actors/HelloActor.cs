using System;
using Akka.Actor;
using RouterLab.Model.Messages;

namespace RouterLab.Actors
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