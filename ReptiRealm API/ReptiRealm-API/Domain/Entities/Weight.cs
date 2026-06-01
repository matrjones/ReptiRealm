using ReptiRealm_API.Domain.Entities.Common;
using ReptiRealm_API.Domain.Enums;
using System.Text.Json.Serialization;

namespace ReptiRealm_API.Domain.Entities
{
    public class Weight : BaseEntity
    {
        #region Variables
        public required DateTime Date { get; set; }
        public required decimal Value { get; set; }
        public required WeightUnit Unit { get; set; } = WeightUnit.Grams;
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
