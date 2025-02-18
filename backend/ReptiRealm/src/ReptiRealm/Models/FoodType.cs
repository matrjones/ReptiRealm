using System.ComponentModel.DataAnnotations;

namespace ReptiRealm.Models
{
    public class FoodType : BaseEntity
    {
        [Required]
        public required string Name { get; set; }
    }
}
