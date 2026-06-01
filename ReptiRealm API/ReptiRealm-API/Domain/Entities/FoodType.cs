using ReptiRealm_API.Domain.Entities.Common;
using System.Text.Json.Serialization;

namespace ReptiRealm_API.Domain.Entities
{
    public class FoodType : BaseEntity
    {
        #region Variables
        public required string Name { get; set; }
        public string? Notes { get; set; }
        #endregion


        #region Foreign Keys
        public string? UserId { get; set; } = null!;
        #endregion


        #region Navigation Properties
        [JsonIgnore]
        public virtual User? User { get; set; }
        [JsonIgnore]
        public virtual ICollection<Feed> Feeds { get; set; } = new List<Feed>();
        #endregion
    }
}
