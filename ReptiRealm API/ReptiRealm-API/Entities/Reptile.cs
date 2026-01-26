using ReptiRealm_API.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReptiRealm_API.Entities
{
    public class Reptile : BaseEntity
    {
        public required string Name { get; set; }
        public char Sex { get; set; }
        public DateTime? DoB { get; set; }
        public string? Notes { get; set; }

        #region Foreign Keys
        public Guid SpeciesId { get; set; }
        #endregion

        #region Navigation Properties
        public virtual Species Species { get; set; } = null!;
        #endregion
    }
}
