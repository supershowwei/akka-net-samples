using System;

namespace Shared.Model.Messages
{
    public sealed class Pull
    {
        private static readonly Lazy<Pull> Lazy = new Lazy<Pull>(() => new Pull());

        private Pull()
        {
        }

        public static Pull Instance => Lazy.Value;
    }
}