using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Inception.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<TEntity> _dbSet;
        private readonly List<INavigation> _navigations;



        public GenericRepository(InceptionDbContext dbContext)
        {
            _dbContext = dbContext;

            _dbSet = dbContext.Set<TEntity>();

            _navigations = _dbContext.Model.FindEntityType(typeof(TEntity))
                .GetNavigations()
                .ToList();
        }



        public IQueryable<TEntity> GetAll()
        {
            return _navigations.Aggregate
                (
                _dbSet.AsNoTracking(),
                (queryable, navigation) => queryable.Include(navigation.Name)
                );
        }



        public async Task<TEntity> GetById(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            foreach (var navigation in _dbContext.Entry(entity).Navigations)
            {
                navigation.Load();
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