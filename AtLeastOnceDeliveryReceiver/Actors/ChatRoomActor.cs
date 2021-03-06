﻿using System;
using Akka;
using Akka.Actor;
using Shared.Model.Messages;

namespace AtLeastOnceDeliveryReceiver.Actors
{
    public class ChatRoomActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            message.Match().With<ReliableDeliveryEnvelope<ChatMessage>>(_ => this.HandlerChatMessage(_));
        }

        private void HandlerChatMessage(ReliableDeliveryEnvelope<ChatMessage> envelope)
        {
            this.Sender.Tell(new ReliableDeliveryAck(envelope.DeliveryId));

            Console.WriteLine($"({envelope.DeliveryId}){envelope.Message.Name} Say: {envelope.Message.Text}");
        }
    }
}