using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Models
{
    public class AccountModel
    {       
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public bool? EmailConfirmed { get; set; } = false;
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool? PhoneNumberConfirmed { get; set; } = false;
        public bool? TwoFactorEnabled { get; set; } = false;
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool? LockoutEnabled { get; set; } = false;
        public int? AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string ReferralLink { get; set; }
        public string ParentId { get; set; }
        public int Level { get; set; }
        public List<AccountModel> Children { get; set; }

    }
}
