using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace wslyvh.Core.Interfaces.Data
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> AsQueryable();

        IEnumerable<T> FindAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        T Single(Expression<Func<T, bool>> predicate);
        T First(Expression<Func<T, bool>> predicate);
        T FindById(object id);

        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);

        void Save();
    }
}
