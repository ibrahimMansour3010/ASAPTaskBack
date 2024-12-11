using ASAPTask.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ASAPTask.Applications.Common.Interfaces
{
    public interface IGenericRepo<T, TType> where TType : struct where T : BaseEntity<TType>
    {
        #region Read Methods 
        IQueryable<T> Get(Expression<Func<T, bool>>? filter = null,
                Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "");
        IQueryable<T> GetNoTrack(Expression<Func<T, bool>>? filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "");
        Task<T?> GetOneAsyncNoTrack(Expression<Func<T, bool>>? filter = null,
            string includeProperties = "");
        Task<T?> GetOneAsync(Expression<Func<T, bool>>? filter = null,
            string includeProperties = "");
        Task<int> CountAsync(Expression<Func<T, bool>>? filter = null);
        Task<IQueryable<T>> GetPaginatedAsync(Expression<Func<T, bool>>? filter = null,
               Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
               int? page = null,
               int? pageSize = null,
               string includeProperties = "");
        #endregion
        #region Write Methods
        Task<T> InsertAsync(T entity);
        T Update(T entity);
        void Remove(T entity);
        #endregion
    }
}
