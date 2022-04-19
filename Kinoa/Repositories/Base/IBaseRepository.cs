using System;
using System.Linq.Expressions;

namespace Kinoa.Repositories.Base
{
    public interface IBaseRepository<TEntity> 
    {
        TEntity FindById(Guid id);
        TEntity[] GetAll();
        TEntity[] GetAll(params Expression<Func<TEntity, object>>[] includes);
        TEntity GetEntityByConditionOrDefault(Expression<Func<TEntity, bool>> condition);
        TEntity GetEntityByConditionOrDefault(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] includes);
        TEntity[] GetAllByCondition(Expression<Func<TEntity, bool>> condition);
        TEntity[] GetAllByCondition(Expression<Func<TEntity, bool>> condition, params Expression<Func<TEntity, object>>[] includes);
        TEntity Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

    }
}
