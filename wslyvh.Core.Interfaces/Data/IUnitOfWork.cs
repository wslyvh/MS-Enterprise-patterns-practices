using System;

namespace wslyvh.Core.Interfaces.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
