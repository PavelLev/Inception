using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Inception.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<TEntity> _dbSet;



        public GenericRepository(InceptionDbContext dbContext)
        {
            _dbContext = dbContext;

            _dbSet = dbContext.Set<TEntity>();
        }



        public IQueryable<TEntity> GetAll()
        {
            var entities = _dbSet.AsNoTracking();

            return entities;
        }



        public async Task<TEntity> GetById(int id, IEnumerable<Expression<Func<TEntity, object>>> propertyExpressions = null)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity != null &&
                propertyExpressions != null)
            {
                foreach (var propertyExpression in propertyExpressions)
                {
                    _dbContext.Entry(entity)
                        .Navigations
                        .First(navigationProperty => navigationProperty.Metadata.Name == propertyExpression.GetPropertyAccess().Name)
                        .Load();
                }
            }

            return entity;
        }



        public async Task Create(TEntity entity)
        {
            await _dbSet.AddAsync(entity);

            await _dbContext.SaveChangesAsync();
        }



        public async Task Update(TEntity entity)
        {
            _dbSet.Update(entity);

            await _dbContext.SaveChangesAsync();
        }



        public async Task Delete(TEntity entity)
        {
            _dbSet.Remove(entity);

            await _dbContext.SaveChangesAsync();
        }
    }
}