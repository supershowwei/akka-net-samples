using System;
using System.Threading;
using Akka.Actor;
using ReceiveTimeoutLab.Actors;
using ReceiveTimeoutLab.Messages;

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
                Thread.Sleep(random.Next(5) * 1000);

                IEnvelope envelope = null;
                switch (random.Next(1000) % 2)
                {
                    case 0:
                        envelope = new ImMessage("abc", 1);
                        break;

                    case 1:
                        envelope = new UrMessage("abc", 1);
                        break;
                }

                actor.Tell(envelope);
            }
        }
    }
}