using System;
using System.Collections.Immutable;
using System.Linq;
using Akka;
using Akka.Actor;
using Akka.Persistence;
using Shared.Model.Messages;

namespace AtLeastOnceDeliveryLab.Actors
{
    public class AtLeastOnceActor : UntypedPersistentActor
    {
        private readonly TimeSpan interval = TimeSpan.FromSeconds(5);
        private decimal count;
        private ImmutableSortedDictionary<decimal, Delivery> unconfirmed = ImmutableSortedDictionary<decimal, Delivery>.Empty;
        private ICancelable redeliverScheduler;

        public override string PersistenceId => this.Self.Path.Name;

        protected override void OnCommand(object message)
        {
            message.Match()
                .With<RedeliveryTick>(this.RedeliverOverdue)
                .With<ChatMessage>(_ => this.HandleChatMessage(_))
                .With<Acknowledgment>(_ => this.HandleAcknowledgment(_));
        }

        protected override void OnRecover(object message)
        {
        }

        protected override void PreStart()
        {
            this.redeliverScheduler = Context.System.Scheduler.ScheduleTellRepeatedlyCancelable(
                new TimeSpan(this.interval.Ticks / 2),
                new TimeSpan(this.interval.Ticks / 2),
                this.Self,
                RedeliveryTick.Instance,
                this.Self);

            base.PreStart();
        }

        protected override void PostStop()
        {
            this.redeliverScheduler?.Cancel();

            base.PostStop();
        }

        private void HandleAcknowledgment(Acknowledgment ack)
        {
            Console.WriteLine($"Remove: {ack.DeliveryId}");

            this.unconfirmed = this.unconfirmed.Remove(ack.DeliveryId);
        }

        private void HandleChatMessage(ChatMessage chatMessage)
        {
            var deliveryId = ++this.count;

            this.Send(
                deliveryId,
                new Delivery(
                    ActorPath.Parse("akka.tcp://sys@localhost:2571/user/chatroom"),
                    new Envelope<ChatMessage>(deliveryId, chatMessage),
                    DateTime.Now));
        }

        private void RedeliverOverdue()
        {
            var deadline = DateTime.Now - this.interval;

            foreach (var entry in this.unconfirmed.Where(e => e.Value.Timestamp <= deadline).ToArray())
            {
                var deliveryId = entry.Key;

                this.Send(deliveryId, entry.Value);
            }
        }

        private void Send(decimal deliveryId, Delivery delivery)
        {
            Console.WriteLine($"Send: {deliveryId}");

            this.unconfirmed = this.unconfirmed.SetItem(
                deliveryId,
                new Delivery(delivery.Destination, delivery.Message, DateTime.Now));

            Context.ActorSelection(delivery.Destination).Tell(delivery.Message);
        }
    }

    public class Delivery
    {
        public Delivery(ActorPath destination, object message, DateTime timestamp)
        {
            this.Destination = destination;
            this.Message = message;
            this.Timestamp = timestamp;
        }

        public ActorPath Destination { get; }

        public object Message { get; }

        public DateTime Timestamp { get; }
    }

    public class RedeliveryTick
    {
        private static readonly Lazy<RedeliveryTick> Lazy = new Lazy<RedeliveryTick>(() => new RedeliveryTick());

        private RedeliveryTick()
        {
        }

        public static RedeliveryTick Instance => Lazy.Value;
    }
}