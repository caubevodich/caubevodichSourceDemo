using System;

namespace Core.Domain.Entities
{
    public class Account : BaseEntity
    {
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public bool? EmailConfirmed { get; set; } = false;
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool? PhoneNumberConfirmed { get; set; } = false;
        public bool TwoFactorEnabled { get; set; } = false;
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool? LockoutEnabled { get; set; } = false;
        public int? AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string ReferralLink { get; set; }
        public string ParentId { get; set; }
        public int Level { get; set; }
        public decimal Balance { get; set; }
        public string VerificationCode { get; set; }
        public DateTime ? VerificationTimeToLive { get; set; }
        public string WalletAddress { get; set; }
    }
}