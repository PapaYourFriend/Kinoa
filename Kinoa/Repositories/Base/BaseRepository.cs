using Kinoa.DataContext;
using Kinoa.Repositories.Common;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Kinoa.Repositories.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly KinoaDataContext _dataContext;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(KinoaDataContext dataContext)
        {
            _dataContext = dataContext;
            _dbSet = _dataContext.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException("Entity was null");
            }

            _dbSet.Add(entity);

            _dataContext.SaveChanges(); 

            return entity;
        }

        public void Delete(TEntity entity)
        {
            if(entity is null)
            {
                throw new ArgumentNullException("Entity was null");
            }

            _dbSet.Remove(entity);

            _dataContext.SaveChanges(); 
        }

        public TEntity FindById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public TEntity[] GetAll()
        {
            return _dbSet.AsNoTracking()
                .ToArray();
        }

        public TEntity[] GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            return _dbSet.AsNoTracking()
                .IncludeMultiple(includes)
                .ToArray();
        }

        public TEntity[] GetAllByCondition(Expression<Func<TEntity, bool>> condition)
        {
            return _dbSet.Where(condition)
                .ToArray();
        }

        public TEntity[] GetAllByCondition(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] includes)
        {
            return _dbSet.IncludeMultiple(includes)
                .Where(condition)
                .ToArray();
        }

        public TEntity GetEntityByConditionOrDefault(Expression<Func<TEntity, bool>> condition)
        {
            return _dbSet.FirstOrDefault(condition);
        }

        public TEntity GetEntityByConditionOrDefault(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] includes)
        {
            return _dbSet.IncludeMultiple(includes)
                .FirstOrDefault(condition);
        }

        public void Update(TEntity entity)
        {
            if(entity is null)
            {
                throw new ArgumentNullException("Entity was null");
            }

            _dataContext.Entry(entity).State = EntityState.Modified;

            _dataContext.SaveChanges();
        }
    }
}
