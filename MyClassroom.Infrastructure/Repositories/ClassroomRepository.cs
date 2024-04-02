using Microsoft.EntityFrameworkCore;
using MyClassroom.Contracts;
using MyClassroom.Contracts.EFCoreFilter;
using MyClassroom.Domain.AggregatesModel.ClassroomAggregate;
using MyClassroom.Domain.SeedWork;
using System.Linq.Expressions;

namespace MyClassroom.Infrastructure.Repositories
{
    public class ClassroomRepository : BaseRepository<Classroom, ClassroomDto, Guid>, IClassroomRepository
    {
        public ClassroomRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<Classroom> GetByIdAsync(Guid id)
        {
            EFCoreFilter<Classroom> filter = new();

            Expression<Func<Classroom, bool>> e = classRoom => classRoom.IsDeleted != false && classRoom.Id == id;

            return await base.GetAsync(filter);
        }

        public Task<List<Classroom>> GetListAsync(Expression<Func<Classroom, bool>> expression, int? limit = null, int? offset = null)
        {
            throw new NotImplementedException();
        }
    }
}
