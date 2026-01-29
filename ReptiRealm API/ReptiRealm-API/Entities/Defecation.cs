using ReptiRealm_API.Entities.Common;
using System.Text.Json.Serialization;

namespace ReptiRealm_API.Entities
{
    public class Defecation : BaseEntity
    {
        public required DateTime Date { get; set; }
        public required string Type { get; set; }
        public string? Notes { get; set; }

        #region Foreign Keys
        public Guid ReptileId { get; set; }
        #endregion

        #region Navigation Properties
        [JsonIgnore]
        public virtual Reptile Reptile { get; set; } = null!;
        #endregion
    }
}
