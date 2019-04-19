using System;
using System.Threading;
using Akka.Actor;

namespace InboxLab.Actors
{
    public partial class HelloActor : ReceiveActor
    {
        public HelloActor()
        {
            this.Receive<string>(
                msg =>
                    {
                        Console.WriteLine($"{this.Self.Path}: Receive {msg}!!!");
                        Thread.Sleep(1000);
                        this.Sender.Tell($"{this.Self.Path}: Hello {msg}!!!");
                    });
        }
    }
}