using JinjiProject.Core.Entities.Abstract;
using JinjiProject.Core.Enums;
using JinjiProject.DataAccess.Interface.Repositories;
using JinjiProject.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JinjiProject.DataAccess.EFCore.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        private readonly AppDbContext appDbContext;
        protected DbSet<T> _table;
        public BaseRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
            _table = appDbContext.Set<T>();
        }

        public async Task<bool> Create(T entity)
        {
            entity.CreatedDate = DateTime.Now;
            entity.Status = Status.Active;
            await _table.AddAsync(entity);
            return await SaveChange();
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool tracking = true)
        {
            if(tracking)
                return await _table.AsNoTracking().ToListAsync();
            else
                return await _table.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllByExpression(Expression<Func<T, bool>> expression)
        {
            var admins = await _table.Where(expression).ToListAsync();
            return admins;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _table.FindAsync(id);
        }

        public async Task<TResult> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> select = null, Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _table;  // SELECT * FROM Post gibi...

            if (include != null)  // JOIN İŞLEMİ
            {
                query = include(query);
            }

            if (where != null) // SELECT * FOM Post WHERE Status = 1 gibi...
            {
                query = query.Where(where);
            }

            if (orderBy != null)
            {
                return await orderBy(query).Select(select).FirstOrDefaultAsync();

            }

            var result = await query.Select(select).FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> select = null, Expression<Func<T, bool>> where = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _table; // SELECT * FROM Post gibi...

            if (where != null)   // SELECT * FOM Post WHERE Status = 1 gibi...
            {
                query = query.Where(where);
            }

            if (include != null)
            {
                query = include(query); // JOIN İŞLEMİ
            }


            if (orderBy != null)  // Sıralama işlemi varsa sıralayıp return edecek yoksa sıralamadan query'i sorgulayıop sonucu liste şeklinde return edecek.
            {

                return await orderBy(query).Select(select).ToListAsync();
            }

            var result = await query.Select(select).ToListAsync();

            return result;
        }

        public Task<bool> HardDelete(T entity)
        {
            _table.Remove(entity);
            return SaveChange();
        }

        public async Task<bool> SaveChange()
        {

            return await appDbContext.SaveChangesAsync() > 0 ;
        }

        public Task<bool> SoftDelete(T entity)
        {
            entity.DeletedDate = DateTime.Now;
            entity.Status = Status.Deleted;
            return SaveChange();
        }

        public Task<bool> Update(T entity)
        {
            entity.ModifiedDate = DateTime.Now;
            entity.Status = Status.Modified;
            return SaveChange();
        }
    }
}
