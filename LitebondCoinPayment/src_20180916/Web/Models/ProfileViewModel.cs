using System.Collections.Generic;
using Core.Domain.Entities;

namespace WebUI.Models
{
    public class ProfileViewModel
    {
        public bool TwoFactorEnabled { get; set; }

        public string ManualEntryKey { get; set; }

        public string QrCodeSetupImageUrl { get; set; }

        public IEnumerable<LoginHistory> LoginHistorys { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }
        public string ReferralId { get; set; }

        public string PhoneNumber { get; set; }

        public string WalletAddress { get; set; }

        public string TwoFactorStatus
        {
            get
            {
                return TwoFactorEnabled ? "Enabled" : "Disabled";
            }
        }
    }
}