using ReptiRealm_API.Entities.Common;

namespace ReptiRealm_API.Entities
{
    public class Shed : BaseEntity
    {
        public required string Name { get; set; }
        public char Rating { get; set; }
        public string? Notes { get; set; }

        #region Navigation Properties
        public virtual ICollection<Reptile> Reptiles { get; set; } = new List<Reptile>();
        #endregion
    }
}
