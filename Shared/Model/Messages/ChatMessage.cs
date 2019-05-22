namespace Shared.Model.Messages
{
    public sealed class ChatMessage
    {
        public ChatMessage(string name, string text)
        {
            this.Name = name;
            this.Text = text;
        }

        public string Name { get; }

        public string Text { get; }
    }
}