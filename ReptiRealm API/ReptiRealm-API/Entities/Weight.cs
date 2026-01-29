using ReptiRealm_API.Entities.Common;
using System.Text.Json.Serialization;

namespace ReptiRealm_API.Entities
{
    public class Weight : BaseEntity
    {
        #region Variables
        public required DateTime Date { get; set; }
        public required decimal Value { get; set; }
        public string Unit { get; set; } = "g";
        public string? Notes { get; set; }
        #endregion


        #region Foreign Keys
        public Guid ReptileId { get; set; }
        #endregion


        #region Navigation Properties
        [JsonIgnore]
        public virtual Reptile Reptile { get; set; } = null!;
        #endregion
    }
}
