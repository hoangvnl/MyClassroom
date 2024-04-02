using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyClassroom.Domain.AggregatesModel.UserClassroomAggregate;

namespace MyClassroom.Infrastructure.EntityConfigurations
{
    public class UserClassroomEntityTypeConfiguration : IEntityTypeConfiguration<UserClassroom>
    {
        public void Configure(EntityTypeBuilder<UserClassroom> userClassroomConfiguration)
        {
            userClassroomConfiguration.HasKey(x => new {x.ClassroomId, x.UserId});

            userClassroomConfiguration
                .Property<int>("UserClassroomJoinTypeId")
                .HasColumnName("UserClassroomJoinTypeId")
                .IsRequired();

            userClassroomConfiguration.Ignore(e => e.Id);

            userClassroomConfiguration.HasOne(e => e.UserClassroomJoinType)
                .WithMany()
                .HasForeignKey("UserClassroomJoinTypeId");
        }
    }
}
