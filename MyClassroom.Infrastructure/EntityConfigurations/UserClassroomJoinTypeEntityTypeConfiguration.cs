using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyClassroom.Domain.AggregatesModel.UserClassroomAggregate;

namespace MyClassroom.Infrastructure.EntityConfigurations
{
    public class UserClassroomJoinTypeEntityTypeConfiguration : IEntityTypeConfiguration<UserClassroomJoinType>
    {
        public void Configure(EntityTypeBuilder<UserClassroomJoinType> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired();
        }
    }
}
