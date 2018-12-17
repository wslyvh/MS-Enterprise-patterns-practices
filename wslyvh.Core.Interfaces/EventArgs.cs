using System;

namespace wslyvh.Core.Interfaces
{
    public class EventArgs<T> : EventArgs
    {
        public T Parameter { get; set; }

        public EventArgs(T input)
        {
            this.Parameter = input;
        }
    }
}
