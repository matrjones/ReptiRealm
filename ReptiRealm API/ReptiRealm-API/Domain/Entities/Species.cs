using ReptiRealm_API.Domain.Entities.Common;
using System.Text.Json.Serialization;

namespace ReptiRealm_API.Domain.Entities
{
    public class Species : BaseEntity
    {
        #region Variables
        public required string Name { get; set; }
        public string? Notes { get; set; }
        #endregion


        #region Foreign Keys
        public string UserId { get; set; } = null!;
        #endregion


        #region Navigation Properties
        [JsonIgnore]
        public virtual User User { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Reptile> Reptiles { get; set; } = new List<Reptile>();
        [JsonIgnore]
        public virtual ICollection<Morph> Morphs { get; set; } = new List<Morph>();
        #endregion
    }
}
