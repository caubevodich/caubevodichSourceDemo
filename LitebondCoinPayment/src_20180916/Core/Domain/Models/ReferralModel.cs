using System;

namespace Core.Domain.Models
{
    public class ReferralModel
    {
        public string Email { get; set; }

        public int Level { get; set; }

        public decimal TotalDeposit { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}