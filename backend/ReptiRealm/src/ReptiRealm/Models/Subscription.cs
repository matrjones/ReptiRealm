using System.ComponentModel.DataAnnotations;

namespace ReptiRealm.Models
{
    public class Subscription : BaseEntity
    {
        [Required]
        public string StripeCustomerId { get; set; }

        [Required]
        public string StripeSubscriptionId { get; set; }

        [Required]
        public string Plan { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime CurrentPeriodEnd { get; set; }

        [Required]
        public bool CancelAtPeriodEnd { get; set; }
    }
} 