using System;

namespace wslyvh.Core.Test.Mock
{
    [Serializable]
    public class SimpleEntityMock
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static SimpleEntityMock CreateDefault()
        {
            return new SimpleEntityMock() { Id = 1, Name = "Test Entity" };
        }
    }
}
