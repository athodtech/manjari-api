using AthodBeTrackApi.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Repositories
{
    public interface IGenericRepository
    {
        bool AddMultipleEntity<TEntity>(IEnumerable<TEntity> entityList) where TEntity : class;
        Task<bool> AddMultipleEntityAsync<TEntity>(IEnumerable<TEntity> entityList) where TEntity : class;
        IDatabaseTransaction BeginTransaction();
        Task<int> Count<TEntity>() where TEntity : class;
        void Delete<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class;
        Task DeleteAsync<TEntity>(object id) where TEntity : class;
        Task DeleteAsync<TEntity>(TEntity entityToDelete) where TEntity : class;
        void Dispose();
        bool Exists<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class;
        Task<bool> ExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class;
        IEnumerable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "") where TEntity : class;
        Task<IEnumerable<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "") where TEntity : class;
        TEntity GetByID<TEntity>(object id) where TEntity : class;
        Task<TEntity> GetByIDAsync<TEntity>(object id) where TEntity : class;
        TEntity GetFirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class;
        Task<TEntity> GetFirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class;
        IQueryable<TEntity> GetIQueryable<TEntity>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "") where TEntity : class;
        int Insert<TEntity>(TEntity entity) where TEntity : class;
        Task<int> InsertAsync<TEntity>(TEntity entity) where TEntity : class;
        long InsertLong<TEntity>(TEntity entity) where TEntity : class;
        Task<long> InsertLongAsync<TEntity>(TEntity entity) where TEntity : class;
        bool RemoveMultipleEntity<TEntity>(IEnumerable<TEntity> removeEntityList) where TEntity : class;
        Task<bool> RemoveMultipleEntityAsync<TEntity>(IEnumerable<TEntity> removeEntityList) where TEntity : class;
        void Update<TEntity>(TEntity entityToUpdate) where TEntity : class;
        Task UpdateAsync<TEntity>(TEntity entityToUpdate) where TEntity : class;
        Task<bool> UpdateMultipleEntityAsync<TEntity>(IEnumerable<TEntity> entityList) where TEntity : class;
    }
}