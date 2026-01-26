using ReptiRealm_API.Entities.Common;

namespace ReptiRealm_API.Entities
{
    public class Feed : BaseEntity
    {
        public required string Name { get; set; }
        public int Amount { get; set; }
        public bool IsEaten { get; set; }
        public string? Notes { get; set; }

        #region Foreign Keys
        public Guid FoodTypeId { get; set; }
        public Guid RegurgitationId { get; set; }
        #endregion

        #region Navigation Properties
        public virtual ICollection<Reptile> Reptiles { get; set; } = new List<Reptile>();
        public virtual FoodType FoodType { get; set; } = null!;
        public virtual Regurgitation Regurgitation { get; set; } = null!;
        #endregion
    }
}
