using System;
using Akka;
using Akka.Actor;
using Akka.Routing;
using Shared.Model.Messages;

namespace PullWorker.Actors
{
    public class ChatRoomActor : UntypedActor
    {
        private readonly ActorPath actorPath = ActorPath.Parse("akka.tcp://sys@localhost:2581/user/pool");
        private readonly IActorRef helloActor;

        public ChatRoomActor()
        {
            this.helloActor = Context.ActorOf(
                Props.Create<HelloActor>()
                    .WithRouter(
                        new ConsistentHashingPool(3000).WithSupervisorStrategy(
                            new OneForOneStrategy(
                                ex =>
                                    {
                                        switch (ex)
                                        {
                                            case ActorInitializationException _:
                                            case ActorKilledException _: return Directive.Stop;
                                            default: return Directive.Restart;
                                        }
                                    }))),
                "hello");
        }

        protected override void OnReceive(object message)
        {
            message.Match()
                .With<Envelope<ChatMessage>>(
                    envelope =>
                        {
                            this.Sender.Tell(new Acknowledgment(envelope.MessageId));

                            Console.Write($"({envelope.MessageId})");
                            this.helloActor.Tell(envelope.Message);
                        });
        }

        protected override void PreStart()
        {
            Context.ActorSelection(this.actorPath).Tell(Pull.Instance);

            base.PreStart();
        }
    }
}