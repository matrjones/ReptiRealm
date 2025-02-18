using System.ComponentModel.DataAnnotations;

namespace ReptiRealm.Models
{
    public class Shed : BaseEntity
    {
        [Required]
        public required DateOnly Date {  get; set; }
        [Required]
        public required char BlueOrShed { get; set; }
        [Required]
        public required char Rating { get; set; }
        public string? Comment { get; set; }
    }
}
