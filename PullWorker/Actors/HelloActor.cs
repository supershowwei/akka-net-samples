using System;
using Akka;
using Akka.Actor;
using Shared.Model.Messages;

namespace PullWorker.Actors
{
    public class HelloActor : UntypedActor
    {
        public HelloActor()
        {
            this.SetReceiveTimeout(TimeSpan.FromSeconds(5));
        }

        protected override void OnReceive(object message)
        {
            message.Match().With<ChatMessage>(_ => this.HandleChatMessage(_));
        }

        private void HandleChatMessage(ChatMessage chatMessage)
        {
            Console.WriteLine($"{chatMessage.Name} Say: {chatMessage.Text} @{this.Self.Path}({chatMessage.ConsistentHashKey})");
        }
    }
}