using System;

namespace wslyvh.Core.Test.Mock
{
    public class SingletonMock : Singleton<SingletonMock>
    {
        SingletonMock() { }

        private readonly int _value = new Random().Next(0, 1000);

        public int Value { get { return _value; } }
    }
}
