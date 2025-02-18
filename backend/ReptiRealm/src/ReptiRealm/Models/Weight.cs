using System.ComponentModel.DataAnnotations;

namespace ReptiRealm.Models
{
    public class Weight : BaseEntity
    {
        [Required]
        public required DateOnly Date { get; set; }
        [Required]
        public required decimal Value { get; set; }
        [Required]
        public required string Unit { get; set; }
        public string? Comment { get; set; }
    }
}
