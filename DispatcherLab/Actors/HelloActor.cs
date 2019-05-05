using System;
using System.Net.Http;
using Akka.Actor;
using DispatcherLab.Model.Messages;

namespace DispatcherLab.Actors
{
    public class HelloActor : ReceiveActor
    {
        public HelloActor()
        {
            this.Receive<Who>(
                who =>
                    {
                        //var sender = this.Sender;

                        //new HttpClient().GetAsync(new Uri("abc")).PipeTo(sender);

                        Console.WriteLine($"{this.Self.Path}: Hello {who.Name}!!!");
                    });
        }

        public static Props Props()
        {
            return Akka.Actor.Props.Create(() => new HelloActor());
        }
    }
}