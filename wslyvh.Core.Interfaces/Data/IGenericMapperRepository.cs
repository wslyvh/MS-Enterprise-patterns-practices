using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace wslyvh.Core.Interfaces.Data
{
    public interface IGenericMapperRepository<TEntity, TDto> where TEntity : class where TDto : class
    {
        IQueryable<TDto> AsQueryable();

        IEnumerable<TDto> FindAll();
        IEnumerable<TDto> Find(Expression<Func<TDto, bool>> predicate);
        TDto Single(Expression<Func<TDto, bool>> predicate);
        TDto First(Expression<Func<TDto, bool>> predicate);
        TDto FindById(object id);

        void Delete(TDto dto);
        void Add(TDto dto);
        void Update(TDto dto);
    }
}
