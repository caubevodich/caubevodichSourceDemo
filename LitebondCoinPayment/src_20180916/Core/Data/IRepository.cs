using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Core.Domain.Entities;

namespace Core.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Insert(T entity);

        void Insert(IEnumerable<T> entities);

        void Update(T entity);

        void Update(IEnumerable<T> entities);

        void Delete(T entity);

        void Delete(Expression<Func<T, bool>> where);

        void Delete(IEnumerable<T> entities);

        T Get(object Id);

        T Get(Expression<Func<T, bool>> where);

        IEnumerable<T> All();

        IEnumerable<T> Find(Expression<Func<T, bool>> where);

        IQueryable<T> Table { get; }
        IQueryable<T> TableNoTracking { get; }
    }
}