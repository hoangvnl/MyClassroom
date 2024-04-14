using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MyClassroom.Domain.SeedWork
{
    public abstract class Entity : IAggregateRoot
    {
        [Required]
        public Guid CreatedBy { get; protected set; }

        [Required]
        public DateTime CreatedDate { get; protected set; } = DateTime.Now;

        [AllowNull]
        public Guid? UpdatedBy { get; protected set; }

        [AllowNull]
        public DateTime? UpdatedDate { get; protected set; }

        public bool IsDeleted { get; protected set; } = false;

        public Entity() { }

        public Entity(Guid userId)
        {
            CreatedBy = userId;
        }

        protected virtual void Update(Guid userId)
        {
            UpdatedBy = userId;
            UpdatedDate = DateTime.Now;
        }

        protected virtual void Delete(Guid userId)
        {
            UpdatedBy = userId;
            UpdatedDate = DateTime.Now;
            IsDeleted = true;
        }
    }

    public abstract class Entity<TKey> : Entity
    {
        [Required, Key]
        public TKey Id { get; protected set; }

        public Entity() : base()
        {
            
        }
        public Entity(Guid userId) : base(userId)
        {
            
        }
    }
}
