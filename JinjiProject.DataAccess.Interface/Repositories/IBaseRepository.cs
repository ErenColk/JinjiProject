using JinjiProject.Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.DataAccess.Interface.Repositories
{
    public interface IBaseRepository<T> where T : class , IBaseEntity
    {
        Task<T> GetByIdAsync(int id);



        Task<IEnumerable<T>> GetAllAsync(bool tracking = true);

        Task<IEnumerable<T>> GetAllByExpression(Expression<Func<T, bool>> expression);

        Task<T> GetFilteredInclude(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task<TResult> GetFilteredFirstOrDefault<TResult>(
            Expression<Func<T, TResult>> select = null,
            Expression<Func<T, bool>> where = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
            );
        Task<List<TResult>> GetFilteredList<TResult>(
       Expression<Func<T, TResult>> select = null,
       Expression<Func<T, bool>> where = null,
       Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
       Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null
       );

        Task<bool> Create(T entity);
        Task<bool> Update(T entity);
        Task<bool> SoftDelete(T entity);
        Task<bool> HardDelete(T entity);
        Task<bool> SaveChange();
    }
}
