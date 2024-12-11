using ASAPTask.Applications.Common.Interfaces;
using ASAPTask.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Presistence.Repositories.GenericRepo
{
    public class GenericRepo<T, TType> : IGenericRepo<T, TType> where TType : struct where T : BaseEntity<TType> 
    {
        private readonly IASAPTaskDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepo(IASAPTaskDbContext context)
        {
            _context = context;
            _dbSet = _context.GetDbSet<T>();
        }
        public virtual IQueryable<T> Get(Expression<Func<T, bool>>? filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
           string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrWhiteSpace(includeProperties))
                query = IncludeProperties(query, includeProperties);

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }
        public virtual IQueryable<T> GetNoTrack(Expression<Func<T, bool>>? filter = null,
         Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
         string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter).AsNoTracking();
            }
            if (!string.IsNullOrWhiteSpace(includeProperties))
                query = IncludeProperties(query, includeProperties);

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }
        public virtual async Task<T?> GetOneAsyncNoTrack(Expression<Func<T, bool>>? filter = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;
            query = query.Where(filter);

            if (!string.IsNullOrWhiteSpace(includeProperties))
                query = IncludeProperties(query, includeProperties);

            return await query.AsNoTracking().FirstOrDefaultAsync();

        }
        public async Task<T> GetOneAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            query = query.Where(filter);

            if (!string.IsNullOrWhiteSpace(includeProperties))
                query = IncludeProperties(query, includeProperties);

            return await query.FirstOrDefaultAsync();

        }
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
                return await query.Where(filter).AsNoTracking().CountAsync();
            else
                return await query.CountAsync();
        }
        public virtual async Task<T> InsertAsync(T entity)
        {
           var result = await _dbSet.AddAsync(entity);
            return result.Entity;
        }
        public virtual T Update(T entity)
        {
            _dbSet.Update(entity);
            return entity;
        }
        #region GenericRepo Methods

        public virtual async Task<IQueryable<T>> GetPaginatedAsync(Expression<Func<T, bool>>? filter = null,
              Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
              int? page = null,
              int? pageSize = null,
              string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrWhiteSpace(includeProperties))
                query = IncludeProperties(query, includeProperties);
            if (page != null)
                query = query.Skip(((int)page - 1) * (int)pageSize).Take((int)pageSize);

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
        #endregion

        #region Private Methods
        private IQueryable<T> IncludeProperties(IQueryable<T> query, string includeProperties)
        {
            foreach (var include in includeProperties.Split(","))
            {
                query = query.Include(include.Trim());
            }
            return query;
        }
        #endregion
    }
}
