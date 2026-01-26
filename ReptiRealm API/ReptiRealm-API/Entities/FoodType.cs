using ReptiRealm_API.Entities.Common;

namespace ReptiRealm_API.Entities
{
    public class FoodType : BaseEntity
    {
        public required string Name { get; set; }

        #region Navigation Properties
        public virtual ICollection<Feed> Feeds { get; set; } = new List<Feed>();
        #endregion
    }
}
