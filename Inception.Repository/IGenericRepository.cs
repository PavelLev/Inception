using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Inception.Repository
{
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> GetAll();

        Task<TEntity> GetById(int id, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions = null);

        Task Create(TEntity entity);

        Task Update(TEntity entity);

        Task Delete(TEntity entity);
    }
}
