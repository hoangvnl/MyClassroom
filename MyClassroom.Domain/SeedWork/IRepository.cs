﻿using MyClassroom.Contracts.EFCoreFilter;
using MyClassroom.Domain.AggregatesModel.ClassroomAggregate;
using System.Linq.Expressions;

namespace MyClassroom.Domain.SeedWork
{
    public interface IRepository<TEntity, TKey> where TEntity : Entity<TKey>
    {
        IUnitOfWork UnitOfWork { get; }
        Task<TEntity> GetAsync(EFCoreFilter<TEntity> eFCoreFilter);
        Task<List<TEntity>> GetListAsync(EFCoreFilter<Classroom> eFCoreFilter);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity);
        Task<TEntity> CreateAsync(TEntity entity);
    }
}
