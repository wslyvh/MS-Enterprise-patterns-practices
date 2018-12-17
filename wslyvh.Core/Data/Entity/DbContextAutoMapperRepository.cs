using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using wslyvh.Core.Interfaces.Data;
using wslyvh.Core.Interfaces.Data.Entity;

namespace wslyvh.Core.Data.Entity
{
    public class DbContextAutoMapperRepository<TEntity, TDto> : IGenericMapperRepository<TEntity, TDto> 
        where TEntity : class
        where TDto : class
    {
        private readonly IDbContextUnitOfWork _context;
        private readonly IDbSet<TEntity> _dbSet;
        private readonly List<TDto> _dtoList;
        private IQueryable<TDto> _dtoSet
        {
            get { return _dtoList.AsQueryable(); }
        }

        public DbContextAutoMapperRepository(IDbContextUnitOfWork context)
        {
            Guard.ArgumentIsNotNull(context, "context");

            _context = context;
            _dbSet = context.Context.Set<TEntity>();
            _dtoList = _dbSet.Select(i => Mapper.Map<TEntity, TDto>(i)).ToList();

            Mapper.CreateMap<TEntity, TDto>();
        }

        public IQueryable<TDto> AsQueryable()
        {
            return _dtoSet;
        }

        public IEnumerable<TDto> FindAll()
        {
            return _dtoSet;
        }

        public IEnumerable<TDto> Find(Expression<Func<TDto, bool>> predicate)
        {
            Guard.ArgumentIsNotNull(predicate, "predicate");

            return _dtoSet.Where(predicate);
        }

        public TDto Single(Expression<Func<TDto, bool>> predicate)
        {
            Guard.ArgumentIsNotNull(predicate, "predicate");

            return _dtoSet.Single(predicate);
        }

        public TDto First(Expression<Func<TDto, bool>> predicate)
        {
            Guard.ArgumentIsNotNull(predicate, "predicate");

            return _dtoSet.First(predicate);
        }

        public TDto FindById(object id)
        {
            Guard.ArgumentIsNotNull(id, "id");

            var entity = _dbSet.Find(new[] { id });
            return Mapper.Map<TEntity, TDto>(entity);
        }

        public void Delete(TDto dto)
        {
            Guard.ArgumentIsNotNull(dto, "dto");

            var entity = Mapper.Map<TDto, TEntity>(dto);
            _dtoList.Remove(dto);
            _dbSet.Remove(entity);
        }

        public void Add(TDto dto)
        {
            Guard.ArgumentIsNotNull(dto, "dto");

            var entity = Mapper.Map<TDto, TEntity>(dto);
            _dtoList.Add(dto);
            _dbSet.Add(entity);
        }

        public virtual void Update(TDto dto)
        {
            Guard.ArgumentIsNotNull(dto, "dto");

            var entity = Mapper.Map<TDto, TEntity>(dto);

            // Implement Attach in own Repository which is aware of the type to make a proper comparison.
            var index = _dtoList.FindIndex(i => i.GetHashCode() == dto.GetHashCode());
            _dtoList[index] = dto;

            _context.Context.Entry(entity).State = EntityState.Modified;
        }
    }
}
