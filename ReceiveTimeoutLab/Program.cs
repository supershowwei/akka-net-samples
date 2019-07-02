using System;
using System.Threading;
using Akka.Actor;
using ReceiveTimeoutLab.Actors;

namespace ReceiveTimeoutLab
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sys = ActorSystem.Create("sys");

            var actor = sys.ActorOf(Props.Create<HelloActor>());

            var random = new Random(Guid.NewGuid().GetHashCode());

            while (true)
            {
                Thread.Sleep(random.Next(8) * 1000);

                actor.Tell(string.Empty);
            }
        }
    }
}