using System;

namespace Core.Domain.Entities
{
    public abstract class AuditableEntity<T> : Entity<T>, IAuditableEntity
    {
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
        
    }
}