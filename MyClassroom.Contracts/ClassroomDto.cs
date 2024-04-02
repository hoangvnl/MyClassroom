using System.ComponentModel.DataAnnotations;

namespace MyClassroom.Contracts
{
    public class ClassroomDto
    {
        public Guid Id { get; set; }   
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public List<GradeStructureDto> Grades { get; protected set; } = new List<GradeStructureDto>();
    }
}
