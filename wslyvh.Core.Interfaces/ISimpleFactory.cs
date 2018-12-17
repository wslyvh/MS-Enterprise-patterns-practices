
namespace wslyvh.Core.Interfaces
{
    public interface ISimpleFactory
    {
        T Create<T>();
    }

    public interface ISimpleFactory<T>
    {
        T Create();
    }
}