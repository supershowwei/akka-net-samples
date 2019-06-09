using System;
using System.Collections.Generic;
using System.Linq;
using Akka;
using Akka.Actor;
using Shared.Model.Messages;

namespace PullModeLab.Actors
{
    public class PoolActor : UntypedActor
    {
        private readonly ActorPath actorPath = ActorPath.Parse("akka.tcp://sys@localhost:2582/user/chatroom");
        private readonly SortedDictionary<decimal, Envelope<ChatMessage>> queue;
        private IActorRef remoteActor;
        private decimal sequence;

        public PoolActor()
        {
            this.queue = new SortedDictionary<decimal, Envelope<ChatMessage>>();
        }

        protected override void OnReceive(object message)
        {
            message.Match()
                .With<ChatMessage>(_ => this.HandleChatMessage(_))
                .With<Acknowledgment>(_ => this.Acknowledge(_))
                .With<Pull>(this.Deliver)
                .With<Terminated>(
                    _ =>
                        {
                            if (_.ExistenceConfirmed && _.AddressTerminated && _.ActorRef.Path == this.actorPath)
                            {
                                this.remoteActor = null;
                            }
                        });
        }

        private void HandleChatMessage(ChatMessage chatMessage)
        {
            if (this.queue.Count == 0) this.Self.Tell(Pull.Instance, ActorRefs.NoSender);

            Console.WriteLine($"Queue: {this.sequence}");

            this.sequence++;

            this.queue.Add(this.sequence, new Envelope<ChatMessage>(this.sequence, chatMessage));
        }

        private void Acknowledge(Acknowledgment ack)
        {
            Console.WriteLine($"Remove: {ack.MessageId}");

            if (this.remoteActor == null)
            {
                this.remoteActor = this.Sender;

                Context.Watch(this.remoteActor);
            }

            this.queue.Remove(ack.MessageId);

            this.Deliver();
        }

        private void Deliver()
        {
            var envelop = this.queue.FirstOrDefault();

            if (envelop.Value == null) return;

            Console.WriteLine($"Send: {envelop.Value.MessageId}");

            if (this.remoteActor != null)
            {
                this.remoteActor.Tell(envelop.Value);
            }
            else
            {
                Context.ActorSelection(this.actorPath).Tell(envelop.Value);
            }
        }
    }
}