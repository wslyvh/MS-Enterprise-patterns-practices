using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using wslyvh.Core.Data.Models;
using wslyvh.Core.Interfaces.Data;

namespace wslyvh.Core.Data.Entity
{
    public class DbContextRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private readonly DbContext _context;
        private readonly IDbSet<T> _dbSet;

        public DbContextRepository(DbContext context)
        {
            Guard.ArgumentIsNotNull(context, "context");

            _context = context;
            _dbSet = context.Set<T>();
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

            return _dbSet.SingleOrDefault(predicate);
        }

        public T First(Expression<Func<T, bool>> predicate)
        {
            Guard.ArgumentIsNotNull(predicate, "predicate");

            return _dbSet.FirstOrDefault(predicate);
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

            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
