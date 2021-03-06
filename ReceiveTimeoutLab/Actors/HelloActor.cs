﻿using System;
using Akka;
using Akka.Actor;
using ReceiveTimeoutLab.Messages;

namespace ReceiveTimeoutLab.Actors
{
    public class HelloActor : UntypedActor
    {
        public HelloActor()
        {
            this.SetReceiveTimeout(TimeSpan.FromSeconds(3));
        }

        protected override void OnReceive(object message)
        {
            message.Match()
                .With<ReceiveTimeout>(_ => { Console.WriteLine("Timeout"); })
                .With<IEnvelope>(
                    _ =>
                        {
                            var a = 1;
                            var b = 0;
                            var c = a / b;
                            Console.WriteLine($"IEnvelope:{_.GetType().Name}");
                        })
                .With<UrMessage>(_ => { Console.WriteLine("UrMessage"); });
        }
    }
}