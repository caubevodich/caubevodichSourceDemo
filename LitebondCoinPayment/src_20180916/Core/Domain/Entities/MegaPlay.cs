using System;

namespace Core.Domain.Entities
{
    public class MegaPlay : Entity<int>
    {
        public string CustId { get; set; }
        public int MegaRoomId { get; set; }
        public int PlayNumber { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedTime { get; set; } = DateTime.UtcNow;
    }
}