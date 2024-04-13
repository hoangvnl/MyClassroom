using Microsoft.EntityFrameworkCore;
using MyClassroom.Domain.AggregatesModel.ClassroomAggregate;
using MyClassroom.Domain.AggregatesModel.RoleAggregate;
using MyClassroom.Domain.AggregatesModel.UserAggregate;
using MyClassroom.Domain.AggregatesModel.UserClassroomAggregate;
using MyClassroom.Infrastructure.EntityConfigurations;

namespace MyClassroom.Infrastructure
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ClassroomEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserClassroomEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserClassroomJoinTypeEntityTypeConfiguration());

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<UserClassroom> UserClassrooms { get; set; }
        public DbSet<UserClassroomJoinType> UserClassroomJoinTypes { get; set; }
    }
}
