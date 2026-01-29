using ReptiRealm_API.Entities.Common;
using System.Text.Json.Serialization;

namespace ReptiRealm_API.Entities
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
