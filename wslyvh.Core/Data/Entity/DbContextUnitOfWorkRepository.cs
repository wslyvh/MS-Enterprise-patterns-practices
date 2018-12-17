using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using wslyvh.Core.Data.Models;
using wslyvh.Core.Interfaces.Data;
using wslyvh.Core.Interfaces.Data.Entity;

namespace wslyvh.Core.Data.Entity
{
    public class DbContextUnitOfWorkRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private readonly IDbContextUnitOfWork _context;
        private readonly IDbSet<T> _dbSet;

        public DbContextUnitOfWorkRepository(IDbContextUnitOfWork context)
        {
            Guard.ArgumentIsNotNull(context, "context");

            _context = context;
            _dbSet = context.Context.Set<T>();
        }

        public IQueryable<T> AsQueryable()
        {
            return _dbSet;
        }

        public IEnumerable<T> FindAll()
        {
            return _dbSet;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            Guard.ArgumentIsNotNull(predicate, "predicate");

            return _dbSet.Where(predicate);
        }

        public T Single(Expression<Func<T, bool>> predicate)
        {
            Guard.ArgumentIsNotNull(predicate, "predicate");

            return _dbSet.Single(predicate);
        }

        public T First(Expression<Func<T, bool>> predicate)
        {
            Guard.ArgumentIsNotNull(predicate, "predicate");

            return _dbSet.First(predicate);
        }

        public T FindById(object id)
        {
            Guard.ArgumentIsNotNull(id, "id");
            return _dbSet.Find(new[] { id });
        }

        public void Delete(T entity)
        {
            Guard.ArgumentIsNotNull(entity, "entity");

            var entry = FindById(entity.Id);
            _dbSet.Remove(entry);
        }

        public void Add(T entity)
        {
            Guard.ArgumentIsNotNull(entity, "entity");

            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            Guard.ArgumentIsNotNull(entity, "entity");

            _context.Context.Entry(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.Commit();
        }

        //public void InsertOrUpdate(T entity)
        //{
        //    if (entity.Id == default(int))
        //    {
        //        // New entity
        //        _dbSet.Add(entity);
        //    }
        //    else
        //    {
        //        // Existing entity
        //        _dbSet.Entry(entity).State = EntityState.Modified;
        //    }
        //}
    }
}
