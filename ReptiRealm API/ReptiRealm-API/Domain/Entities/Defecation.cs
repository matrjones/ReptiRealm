using ReptiRealm_API.Domain.Entities.Common;
using ReptiRealm_API.Domain.Enums;
using System.Text.Json.Serialization;

namespace ReptiRealm_API.Domain.Entities
{
    public class Defecation : BaseEntity
    {
        #region Variables
        public required DateTime Date { get; set; }
        public required DefecationType Type { get; set; } = DefecationType.Faeces;
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
