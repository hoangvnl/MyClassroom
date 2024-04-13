using Microsoft.EntityFrameworkCore;
using MyClassroom.Contracts.EFCoreFilter;
using MyClassroom.Domain.SeedWork;
using System.Linq.Expressions;
using MyClassroom.Domain.AggregatesModel.ClassroomAggregate;

namespace MyClassroom.Infrastructure.Repositories
{
    public abstract class BaseRepository<TEntity, TContract, TKey> : IRepository<TEntity, TKey> where TEntity : Entity<TKey>, IAggregateRoot
    {
        protected readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return (IUnitOfWork)_dbContext;
            }
        }

        protected virtual IEnumerable<Expression<Func<TEntity, object>>> GetAllRelatedObjects()
        {
            return Enumerable.Empty<Expression<Func<TEntity, object>>>();
        }

        private IQueryable<TEntity> BaseQuery(EFCoreFilter<TEntity> eFCoreFilter)
        {
            IQueryable<TEntity> entities = _dbContext.Set<TEntity>();

            if (eFCoreFilter.Filters is { } filter)
            {
                entities.Where(filter);
            }

            if (eFCoreFilter.Includes?.Any() == true)
            {
                foreach (var includeExpression in eFCoreFilter.Includes)
                {
                    entities = entities.Include(includeExpression);
                }
            }
            else
            {
                foreach (var includeExpression in GetAllRelatedObjects())
                {
                    entities = entities.Include(includeExpression);
                }
            }

            if (eFCoreFilter.Sorts is { Count: > 0 } sorts)
            {
                IOrderedQueryable<TEntity> orderedEntities = null;

                foreach (var column in sorts)
                {
                    if (column.Value == SortOrder.Ascending)
                    {
                        orderedEntities = orderedEntities == null
                            ? entities.OrderBy(column.Key)
                            : orderedEntities.ThenBy(column.Key);
                    }
                    else
                    {
                        orderedEntities = orderedEntities == null
                            ? entities.OrderByDescending(column.Key)
                            : orderedEntities.ThenByDescending(column.Key);
                    }
                }

                entities = orderedEntities ?? entities;
            }

            return entities;
        }

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public virtual Task<TEntity> DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<TEntity> GetAsync(EFCoreFilter<TEntity> eFCoreFilter)
        {
            var query = BaseQuery(eFCoreFilter);
            var entity = await query.FirstOrDefaultAsync();

            return entity;
        }

        public virtual Task<TEntity> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> GetListAsync(EFCoreFilter<Classroom> eFCoreFilter)
        {
            throw new NotImplementedException();
        }
    }
}
