using System;

namespace Core.Domain.Entities
{
    public class LoginHistory : Entity<int>
    {
        public int UserId { get; set; }

        public string IPAddress { get; set; }

        public string UserAgent { get; set; }

        public DateTime LoginTime { get; set; } = DateTime.UtcNow;
    }
}