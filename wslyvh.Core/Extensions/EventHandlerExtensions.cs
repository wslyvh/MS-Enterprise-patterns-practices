using System;

namespace wslyvh.Core.Extensions
{
    using wslyvh.Core.Interfaces;

    public static class EventHandlerExtensions
    {
        public static EventArgs<T> CreateArgs<T>(this EventHandler<EventArgs<T>> handler, T input)
        {
            return new EventArgs<T>(input);
        }

        public static ReadOnlyEventArgs<T> CreateReadOnlyArgs<T>(this EventHandler<ReadOnlyEventArgs<T>> handler, T input)
        {
            return new ReadOnlyEventArgs<T>(input);
        }
    }
}
