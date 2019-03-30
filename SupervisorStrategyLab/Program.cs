using System;
using Akka.Actor;
using SupervisorStrategyLab.Actors;

namespace SupervisorStrategyLab
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sys = ActorSystem.Create("sys");

            var routerConfig = ScalableRoundRobinPool.Create(100, 2);

            var childActor2 = sys.ActorOf(Props.Create<ChildActor>().WithRouter(routerConfig), "child2");

            var childActor3 = sys.ActorOf(Props.Create<ChildActor>(), "child3");
            
            string cmd;
            while ((cmd = Console.ReadLine()) != "exit")
            {
                //childActor2.Tell(cmd);
                //childActor2.Tell(new Broadcast(cmd));
                childActor3.Tell(cmd);
            }
        }
    }
}