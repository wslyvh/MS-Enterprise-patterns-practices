using System;

namespace wslyvh.Core
{
    public class ReadOnlyEventArgs<T> : EventArgs
    {
        public T Parameter { get; private set; }

        public ReadOnlyEventArgs(T input)
        {
            Parameter = input;
        }
    }
}
