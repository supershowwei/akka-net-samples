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
                    actor.Tell(new ChatMessage("Johnny", "Hello"));
                }

                Thread.Sleep(1000);
            }
        }
    }
}