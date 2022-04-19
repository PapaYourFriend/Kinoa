using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Kinoa.Repositories.Common
{
    public static class IncludeExtension
    {
        public static IQueryable<TEntity> IncludeMultiple<TEntity>(this IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
    where TEntity : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(query,
                          (current, include) => current.Include(include));
            }

            return query;
        }
    }
}
