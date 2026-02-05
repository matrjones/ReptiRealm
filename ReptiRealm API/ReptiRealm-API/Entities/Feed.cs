using ReptiRealm_API.Entities.Common;
using System.Text.Json.Serialization;

namespace ReptiRealm_API.Entities
{
    public class Feed : BaseEntity
    {
        #region Variables
        public required DateTime Date { get; set; }
        public required int Amount { get; set; } = 1;
        public required bool IsEaten { get; set; } = true;
        public string? Notes { get; set; }
        #endregion


        #region Foreign Keys
        public Guid ReptileId { get; set; }
        public Guid FoodTypeId { get; set; }
        public Guid? RegurgitationId { get; set; }
        #endregion


        #region Navigation Properties
        [JsonIgnore]
        public virtual Reptile Reptile { get; set; } = null!;
        public virtual FoodType FoodType { get; set; } = null!;
        public virtual Regurgitation? Regurgitation { get; set; }
        #endregion
    }
}
