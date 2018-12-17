using System;

namespace wslyvh.Core.Test.Mock
{
    public class TestServiceMock : ITestService
    {
        public string Get()
        {
            return "TestServiceMock";
        }

        public string Get(string value)
        {
            if (value == null) throw new ArgumentNullException("value");

            return string.Format("TestServiceMock: {0}", value);
        }

        public void GetWithException()
        {
            throw new ApplicationException();
        }
    }
}
