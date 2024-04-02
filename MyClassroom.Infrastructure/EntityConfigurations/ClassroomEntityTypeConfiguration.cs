using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyClassroom.Domain.AggregatesModel.ClassroomAggregate;
using MyClassroom.Domain.AggregatesModel.UserClassroomAggregate;

namespace MyClassroom.Infrastructure.EntityConfigurations
{
    public class ClassroomEntityTypeConfiguration : IEntityTypeConfiguration<Classroom>
    {
        public void Configure(EntityTypeBuilder<Classroom> ClassroomConfiguration)
        {
            ClassroomConfiguration.HasKey(x => x.Id);

            ClassroomConfiguration
                .HasMany(e => e.Users)
                .WithMany(e => e.Classrooms)
                .UsingEntity<UserClassroom>();
        }
    }
}
