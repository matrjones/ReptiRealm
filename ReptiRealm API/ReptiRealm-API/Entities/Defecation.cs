using ReptiRealm_API.Entities.Common;

namespace ReptiRealm_API.Entities
{
    public class Defecation : BaseEntity
    {
        public required string Type { get; set; }
        public string? Notes { get; set; }

        #region Navigation Properties
        public virtual ICollection<Reptile> Reptiles { get; set; } = new List<Reptile>();
        #endregion
    }
}
