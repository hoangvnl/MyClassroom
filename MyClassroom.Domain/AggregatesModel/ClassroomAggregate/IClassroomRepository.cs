using System.Linq.Expressions;

namespace MyClassroom.Domain.AggregatesModel.ClassroomAggregate
{
    public interface IClassroomRepository
    {
        Task<Classroom> CreateAsync(Classroom room);
        Task<Classroom> UpdateAsync(Classroom room);
        Task<Classroom> GetByIdAsync(Guid id);
        Task<List<Classroom>> GetListAsync(Expression<Func<Classroom, bool>> expression, int? limit = null, int? offset = null);
    }
}
