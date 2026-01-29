using ReptiRealm_API.Entities.Common;
using System.Text.Json.Serialization;

namespace ReptiRealm_API.Entities
{
    public class Morph : BaseEntity
    {
        #region Variables
        public required string Name { get; set; }
        public string? Notes { get; set; }
        #endregion


        #region Foreign Keys
        public Guid SpeciesId { get; set; }
        #endregion


        #region Navigation Properties
        public virtual Species Species { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Reptile> Reptiles { get; set; } = new List<Reptile>();
        #endregion
    }
}
