using ReptiRealm_API.Domain.Entities.Common;
using System.Text.Json.Serialization;

namespace ReptiRealm_API.Domain.Entities
{
    public class Regurgitation : BaseEntity
    {
        #region Variables
        public string? Notes { get; set; }
        #endregion


        #region Navigation Properties
        [JsonIgnore]
        public virtual Feed Feed { get; set; } = null!;
        #endregion
    }
}
