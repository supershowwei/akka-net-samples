﻿namespace SerializerLab2.Model.Messages
{
    internal sealed class Hello
    {
        public Hello(string message)
        {
            this.Message = message;
        }

        public string Message { get; }
    }
}