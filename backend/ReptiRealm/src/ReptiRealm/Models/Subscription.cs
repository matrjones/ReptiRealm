using System.ComponentModel.DataAnnotations;

namespace ReptiRealm.Models
{
    public class Subscription : BaseEntity
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public string Interval { get; set; }
        [Required]
        public string PlanName { get; set; }
    }
} 