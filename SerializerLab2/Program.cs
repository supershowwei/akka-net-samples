using System;
using Akka.Actor;
using SerializerLab2.Actors;
using SerializerLab2.Model.Messages;

namespace SerializerLab2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sys = ActorSystem.Create("sys");

            sys.ActorSelection("akka.tcp://sys@localhost:2552/user/hello").Tell(new Hello("Hello World!"));

            Console.ReadLine();
        }
    }
}