using System;
using System.Threading;
using Akka.Actor;
using PullModeLab.Actors;
using Shared.Model.Messages;

namespace PullModeLab
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sys = ActorSystem.Create("sys");

            var actor = sys.ActorOf(Props.Create<PoolActor>(), "pool");

            while (true)
            {
                if (DateTime.Now.Millisecond % 3 == 0)
                {
                    actor.Tell(
                        new ChatMessage("Johnny", "Hello", new OpeningPeriod(TimeSpan.Parse("09:00:00"), TimeSpan.Parse("04:30:00"))));
                }

                Thread.Sleep(1000);
            }
        }
    }
}