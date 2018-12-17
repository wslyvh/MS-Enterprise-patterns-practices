using System.Data.Entity;

namespace wslyvh.Core.Interfaces.Data.Entity
{
    public interface IDbContextUnitOfWork : IUnitOfWork
    {
        DbContext Context { get; }
    }
}
