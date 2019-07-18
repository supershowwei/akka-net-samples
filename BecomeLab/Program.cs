using System;
using Akka.Actor;
using BecomeLab.Actors;

namespace BecomeLab
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sys = ActorSystem.Create("sys");

            var actor = sys.ActorOf(Props.Create<TestActor>());

            actor.Tell("111");

            Console.ReadKey();

            actor.Tell("222");

            Console.ReadKey();

            actor.Tell("123");

            Console.ReadKey();

            actor.Tell("333");
            actor.Tell("444");

            Console.ReadKey();
        }
    }
}