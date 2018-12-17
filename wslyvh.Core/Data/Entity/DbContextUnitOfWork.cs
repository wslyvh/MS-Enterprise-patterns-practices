using System;
using System.Data.Entity;
using wslyvh.Core.Interfaces.Data.Entity;

namespace wslyvh.Core.Data.Entity
{
    public class DbContextUnitOfWork : IDbContextUnitOfWork
    {
        private DbContext _context;
        private bool _disposed;

        public DbContext Context
        {
            get { return _context; }
        }

        public DbContextUnitOfWork(DbContext context)
        {
            Guard.ArgumentIsNotNull(context, "context");

            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        #region IDisposable members
        ~DbContextUnitOfWork()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (!disposing)
                return;

            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }

            _disposed = true;
        }
        #endregion
    }
}
