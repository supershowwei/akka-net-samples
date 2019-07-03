using System;
using Akka;
using Akka.Actor;

namespace BecomeLab.Actors
{
    public class TestActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            this.Abc(message);
        }

        private void Abc(object message)
        {
            message.Match().With<int>(_ =>
                {
                    Console.WriteLine(_);

                    this.Become(this.Def);
                });
        }

        private void Def(object message)
        {
            message.Match().With<string>(_ =>
                {
                    Console.WriteLine(_);

                    if (_ == "123")
                    {
                        var a = 1;
                        var b = 0;
                        var c = a / b;
                    }
                });
        }
    }
}