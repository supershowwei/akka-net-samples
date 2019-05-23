using System;
using System.Collections.Immutable;
using System.Linq;
using Akka;
using Akka.Actor;
using Shared.Model.Messages;

namespace PullModeLab.Actors
{
    public class PoolActor : UntypedActor
    {
        private readonly ActorPath actorPath = ActorPath.Parse("akka.tcp://sys@localhost:2582/user/chatroom");
        private decimal count;
        private ImmutableSortedDictionary<decimal, Envelope<ChatMessage>> queue = ImmutableSortedDictionary<decimal, Envelope<ChatMessage>>.Empty;

        protected override void OnReceive(object message)
        {
            message.Match()
                .With<ChatMessage>(_ => this.HandleChatMessage(_))
                .With<Acknowledgment>(_ => this.Acknowledge(_))
                .With<Pull>(this.Deliver);
        }

        protected override void PreStart()
        {
            this.Self.Tell(Pull.Instance);

            base.PreStart();
        }

        private void HandleChatMessage(ChatMessage chatMessage)
        {
            var messageId = ++this.count;

            Console.WriteLine($"Queue: {messageId}");

            this.queue = this.queue.SetItem(messageId, new Envelope<ChatMessage>(messageId, chatMessage));
        }

        private void Acknowledge(Acknowledgment ack)
        {
            Console.WriteLine($"Remove: {ack.MessageId}");

            this.queue = this.queue.Remove(ack.MessageId);

            this.Deliver();
        }

        private void Deliver()
        {
            var envelop = this.queue.FirstOrDefault();

            if (envelop.Value == null)
            {
                Console.WriteLine("Wait envelop");

                Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(1), this.Self, Pull.Instance, ActorRefs.NoSender);
            }
            else
            {
                Console.WriteLine($"Send: {envelop.Value.MessageId}");

                Context.ActorSelection(this.actorPath).Tell(envelop.Value);
            }
        }
    }
}