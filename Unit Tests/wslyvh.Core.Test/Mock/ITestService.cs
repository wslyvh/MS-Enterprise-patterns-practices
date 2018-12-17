namespace wslyvh.Core.Test.Mock
{
    public interface ITestService
    {
        string Get();

        string Get(string value);

        void GetWithException();
    }
}