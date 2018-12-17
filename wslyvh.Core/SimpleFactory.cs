using System;
using wslyvh.Core.Interfaces;

namespace wslyvh.Core
{
    public class SimpleFactory : ISimpleFactory
    {
        public T Create<T>()
        {
            return Activator.CreateInstance<T>();
        }
    }

    public class SimpleFactory<T> : ISimpleFactory<T>
    {
        public T Create()
        {
            return Activator.CreateInstance<T>();
        }
    }
}
