using ReptiRealm_API.Entities.Common;
using ReptiRealm_API.Enums;
using System.Text.Json.Serialization;

namespace ReptiRealm_API.Entities
{
    public class Reptile : BaseEntity
    {
        #region Variables
        public required string Name { get; set; }
        public required Sex Sex { get; set; } = Sex.Unknown;
        public DateTime? DateOfBirth { get; set; }
        public string? Notes { get; set; }
        #endregion


        #region Foreign Keys
        public string UserId { get; set; } = null!;
        public Guid? SpeciesId { get; set; }
        #endregion


        #region Navigation Properties
        [JsonIgnore]
        public virtual User User { get; set; } = null!;
        public virtual Species? Species { get; set; }
        public virtual ICollection<Morph> Morphs { get; set; } = new List<Morph>();
        public virtual ICollection<Feed> Feeds { get; set; } = new List<Feed>();
        public virtual ICollection<Shed> Sheds { get; set; } = new List<Shed>();
        public virtual ICollection<Weight> Weights { get; set; } = new List<Weight>();
        public virtual ICollection<Defecation> Defecations { get; set; } = new List<Defecation>();
        #endregion
    }
}
