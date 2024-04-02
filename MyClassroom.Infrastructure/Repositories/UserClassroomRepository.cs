﻿using Microsoft.EntityFrameworkCore;
using MyClassroom.Domain.AggregatesModel.UserClassroomAggregate;
using MyClassroom.Domain.SeedWork;

namespace MyClassroom.Infrastructure.Repositories
{
    public class UserClassroomRepository : IUserClassroomRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserClassroomRepository(ApplicationDbContext dbContext)
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

        public async Task<UserClassroom> CreateAsync(UserClassroom userClassroom)
        {
            try
            {
                var returnValue = _dbContext.UserClassrooms.Add(userClassroom).Entity;
                await _dbContext.SaveChangesAsync();
                return returnValue;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<IList<Guid>> GetAllClassroomIdsByUserIdAsync(Guid userId)
        {
            var ClassroomIds = await _dbContext.UserClassrooms.Where(u => u.UserId == userId).Select(i => i.ClassroomId).ToListAsync() ?? new List<Guid>();
            return ClassroomIds;
        }

        public async Task<IList<Guid>> GetAllUserIdsByClassroomIdAsync(Guid ClassroomId)
        {
            var userIds = await _dbContext.UserClassrooms.Where(u => u.ClassroomId == ClassroomId).Select(i => i.UserId).ToListAsync() ?? new List<Guid>();
            return userIds;
        }
    }
}
