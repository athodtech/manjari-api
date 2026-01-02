using AthodBeTrackApi.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using AthodBeTrackApi.Repositories.Generic;

namespace AthodBeTrackApi.Repositories
{

    public class GenericRepository : IGenericRepository
    {
        protected readonly AthodDbContext _dbContext = null;
        public GenericRepository()
        {
            _dbContext = new AthodDbContext();
        }
        public virtual IEnumerable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "") where TEntity : class
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
        public virtual async Task<IEnumerable<TEntity>> GetAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "") where TEntity : class
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public virtual TEntity GetByID<TEntity>(object id) where TEntity : class
        {
            return _dbContext.Set<TEntity>().Find(id);
        }
        public async Task<TEntity> GetByIDAsync<TEntity>(object id) where TEntity : class
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public virtual TEntity GetFirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(filter);
        }
        public async Task<TEntity> GetFirstOrDefaultAsync<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync(filter);
        }

        public virtual bool Exists<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class
        {
            if (filter == null) return false;
            return (_dbContext.Set<TEntity>().Any(filter));
        }
        public async Task<bool> ExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null) where TEntity : class
        {
            if (filter == null) return false;
            return await (_dbContext.Set<TEntity>().AnyAsync(filter));
        }

        public virtual int Insert<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                _dbContext.Set<TEntity>().Add(entity);
                _dbContext.SaveChanges();
                int ret = 0;
                PropertyInfo key = typeof(TEntity).GetProperties().FirstOrDefault(p => p.CustomAttributes.Any(attr => attr.AttributeType == typeof(KeyAttribute)));

                if (key != null)
                {
                    ret = (int)key.GetValue(entity, null);
                }
                return ret;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }
        public virtual long InsertLong<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                _dbContext.Set<TEntity>().Add(entity);
                _dbContext.SaveChanges();
                long ret = 0;
                PropertyInfo key = typeof(TEntity).GetProperties().FirstOrDefault(p => p.CustomAttributes.Any(attr => attr.AttributeType == typeof(KeyAttribute)));

                if (key != null)
                {
                    ret = (long)key.GetValue(entity, null);
                }
                return ret;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }
        public async Task<int> InsertAsync<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                await _dbContext.Set<TEntity>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                int ret = 0;
                PropertyInfo key = typeof(TEntity).GetProperties().FirstOrDefault(p => p.CustomAttributes.Any(attr => attr.AttributeType == typeof(KeyAttribute)));

                if (key != null)
                {
                    ret = (int)key.GetValue(entity, null);
                }
                return ret;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public async Task<long> InsertLongAsync<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                await _dbContext.Set<TEntity>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                long ret = 0;
                PropertyInfo key = typeof(TEntity).GetProperties().FirstOrDefault(p => p.CustomAttributes.Any(attr => attr.AttributeType == typeof(KeyAttribute)));

                if (key != null)
                {
                    ret = (long)key.GetValue(entity, null);
                }
                return ret;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }
        public virtual bool AddMultipleEntity<TEntity>(IEnumerable<TEntity> entityList) where TEntity : class
        {
            bool flag;
            if (entityList == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                _dbContext.Set<TEntity>().AddRange(entityList);
                _dbContext.SaveChanges();
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }
        public async Task<bool> AddMultipleEntityAsync<TEntity>(IEnumerable<TEntity> entityList) where TEntity : class
        {
            bool flag;
            if (entityList == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                await _dbContext.Set<TEntity>().AddRangeAsync(entityList);
                await _dbContext.SaveChangesAsync();
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }
        public virtual bool RemoveMultipleEntity<TEntity>(IEnumerable<TEntity> removeEntityList) where TEntity : class
        {
            bool flag;
            if (removeEntityList == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                _dbContext.Set<TEntity>().RemoveRange(removeEntityList);
                _dbContext.SaveChanges();
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }
        public virtual async Task<bool> RemoveMultipleEntityAsync<TEntity>(IEnumerable<TEntity> removeEntityList) where TEntity : class
        {
            var flag = false;
            if (removeEntityList == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                _dbContext.Set<TEntity>().RemoveRange(removeEntityList);
                await _dbContext.SaveChangesAsync();
                flag = true;
            }
            catch (Exception)
            {
                throw;
            }
            return flag;
        }
        public virtual async Task DeleteAsync<TEntity>(object id) where TEntity : class
        {
            TEntity entityToDelete = await _dbContext.Set<TEntity>().FindAsync(id);
            await DeleteAsync(entityToDelete);
            await _dbContext.SaveChangesAsync();
        }
        public virtual void Delete<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            var query = _dbContext.Set<TEntity>().Where(filter);
            _dbContext.Set<TEntity>().RemoveRange(query);

        }

        public virtual async Task DeleteAsync<TEntity>(TEntity entityToDelete) where TEntity : class
        {
            if (entityToDelete == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                _dbContext.Set<TEntity>().Remove(entityToDelete);
                await _dbContext.SaveChangesAsync();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public virtual void Update<TEntity>(TEntity entityToUpdate) where TEntity : class
        {
            if (entityToUpdate == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }
        public async virtual Task UpdateAsync<TEntity>(TEntity entityToUpdate) where TEntity : class
        {
            if (entityToUpdate == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public async virtual Task<bool> UpdateMultipleEntityAsync<TEntity>(IEnumerable<TEntity> entityList) where TEntity : class
        {
            bool flag;
            if (entityList == null)
            {
                throw new ArgumentNullException("entity");
            }
            try
            {
                _dbContext.Set<IEnumerable<TEntity>>().UpdateRange(entityList);
                //_dbContext.Entry(entityList).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                flag = true;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
            return flag;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    _dbContext.Dispose();
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual IQueryable<TEntity> GetIQueryable<TEntity>(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "") where TEntity : class
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>().AsNoTracking();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }
        public async Task<int> Count<TEntity>() where TEntity : class
        {
            return await _dbContext.Set<TEntity>().CountAsync();
        }

        public IDatabaseTransaction BeginTransaction()
        {
            return new EntityDatabaseTransaction(_dbContext);
        }

    }
}
