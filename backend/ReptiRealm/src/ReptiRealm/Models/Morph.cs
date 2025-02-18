using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ReptiRealm.Models
{
    public class Morph : BaseEntity
    {
        [Required]
        public required string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Reptile>? Reptiles { get; set; }

    }
}
