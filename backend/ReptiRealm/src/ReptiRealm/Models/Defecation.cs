using System.ComponentModel.DataAnnotations;

namespace ReptiRealm.Models
{
    public class Defecation :BaseEntity
    {
        [Required]
        public required DateOnly Date { get; set; }
        [Required]
        public required char Type { get; set; }
        public string? Comment { get; set; }
    }
}
