using System.ComponentModel.DataAnnotations;

namespace ReptiRealm.Models
{
    public class Species : BaseEntity
    {
        [Required]
        public required string Name { get; set; }
    }
}
