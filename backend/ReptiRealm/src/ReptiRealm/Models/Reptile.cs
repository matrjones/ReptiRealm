using System.ComponentModel.DataAnnotations;

namespace ReptiRealm.Models
{
    public class Reptile : BaseEntity
    {
        [Required]
        public required string Name { get; set; }
        public char? Sex { get; set; }
        public DateOnly? DOB { get; set; }
        public virtual Species? Species { get; set; }
        public virtual ICollection<Shed>? Sheds { get; set; }
        public virtual ICollection<Weight>? Weights { get; set; }
        public virtual ICollection<Defecation>? Defecations { get; set; }
        public virtual ICollection<Feed>? Feeds { get; set; }
        public virtual ICollection<Morph>? Morphs { get; set; }

    }
}
