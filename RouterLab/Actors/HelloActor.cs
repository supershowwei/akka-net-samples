using System;
using Akka.Actor;
using RouterLab.Model.Messages;

namespace RouterLab.Actors
{
    public class HelloActor : ReceiveActor
    {
        public HelloActor()
        {
            this.Receive<Who>(
                who =>
                    {
                        if (DateTime.Now.Millisecond % 2 == 1)
                        {
                            var a = 1;
                            var b = 0;
                            var c = a / b;
                        }

                        Console.WriteLine($"{this.Self.Path}: Hello {who.Name}!!!");
                    });
        }

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new HelloActor());
        }

        protected override void PreRestart(Exception reason, object message)
        {
            base.PreRestart(reason, message);
        }
    }
}