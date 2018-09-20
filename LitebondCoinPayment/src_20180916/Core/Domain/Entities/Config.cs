using System;

namespace Core.Domain.Entities
{
    public class Config : AuditableEntity<int>
    {
        public string Value { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedTime { get; set; } = DateTime.UtcNow;
    }
}