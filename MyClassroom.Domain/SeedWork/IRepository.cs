using MyClassroom.Contracts.EFCoreFilter;
using System.Linq.Expressions;

namespace MyClassroom.Domain.SeedWork
{
    public interface IRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        IUnitOfWork UnitOfWork { get; }
        Task<TEntity> GetAsync(EFCoreFilter<TEntity> eFCoreFilter);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity);
        Task<TEntity> CreateAsync(TEntity entity);
    }
}
