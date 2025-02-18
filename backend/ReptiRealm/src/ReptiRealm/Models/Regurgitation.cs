using System.ComponentModel.DataAnnotations;

namespace ReptiRealm.Models
{
    public class Regurgitation : BaseEntity
    {
        public string? Comment { get; set; }
        [Required]
        public required virtual Feed Feed { get; set; }
    }
}
