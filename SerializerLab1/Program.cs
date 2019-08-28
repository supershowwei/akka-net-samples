using System;
using Akka.Actor;
using SerializerLab1.Actors;

namespace SerializerLab1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sys = ActorSystem.Create("sys");

            sys.ActorOf<HelloActor>("hello");

            Console.ReadLine();
        }
    }
}