using System;
using Akka.Routing;

namespace Shared.Model.Messages
{
    public sealed class ChatMessage : IConsistentHashable
    {
        public ChatMessage(string name, string text)
        {
            this.Name = name;
            this.Text = text;
            this.ConsistentHashKey = new Random(Guid.NewGuid().GetHashCode()).Next(10).ToString();
        }

        public string Name { get; }

        public string Text { get; }

        public object ConsistentHashKey { get; }
    }
}