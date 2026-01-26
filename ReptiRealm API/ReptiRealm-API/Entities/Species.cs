using ReptiRealm_API.Entities.Common;

namespace ReptiRealm_API.Entities
{
    public class Species : BaseEntity
    {
        public required string Name { get; set; }

        #region Navigation Properties
        public virtual ICollection<Reptile> Reptiles { get; set; } = new List<Reptile>();
        #endregion
    }
}
