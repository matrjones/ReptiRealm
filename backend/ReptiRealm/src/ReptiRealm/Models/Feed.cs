using System.ComponentModel.DataAnnotations;

namespace ReptiRealm.Models
{
    public class Feed : BaseEntity
    {
        [Required]
        public required DateOnly Date { get; set; }
        [Required]
        public required int Number { get; set; }
        [Required]
        public required bool Eaten { get; set; }
        public string? Comment { get; set; }
        [Required]
        public required virtual FoodType FoodType { get; set; }
    }
}
