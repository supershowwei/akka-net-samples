using System;
using Akka;
using Akka.Actor;
using Newtonsoft.Json.Linq;
using SerializerLab1.Model.Messages;

namespace SerializerLab1.Actors
{
    internal class HelloActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            message.Match()
                .With<Hello>(_ => Console.WriteLine(_.Message))
                .With<JObject>(
                    _ =>
                        {
                            var a = _.ToObject<Hello>();
                        })
                .Default(
                    _ =>
                        {
                            var a = _;
                        });
        }
    }
}