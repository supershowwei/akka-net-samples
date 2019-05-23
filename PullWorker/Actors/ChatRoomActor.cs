using System;
using Akka;
using Akka.Actor;
using Shared.Model.Messages;

namespace PullWorker.Actors
{
    public class ChatRoomActor : UntypedActor
    {
        private readonly ActorPath actorPath = ActorPath.Parse("akka.tcp://sys@localhost:2581/user/pool");

        protected override void OnReceive(object message)
        {
            message.Match().With<Envelope<ChatMessage>>(_ => this.HandlerChatMessage(_));
        }

        protected override void PreStart()
        {
            Context.ActorSelection(this.actorPath).Tell(Pull.Instance);

            base.PreStart();
        }

        private void HandlerChatMessage(Envelope<ChatMessage> envelope)
        {
            this.Sender.Tell(new Acknowledgment(envelope.MessageId));

            Console.WriteLine($"({envelope.MessageId}){envelope.Message.Name} Say: {envelope.Message.Text}");
        }
    }
}