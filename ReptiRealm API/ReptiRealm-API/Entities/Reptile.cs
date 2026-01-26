using ReptiRealm_API.Entities.Common;

namespace ReptiRealm_API.Entities
{
    public class Reptile : BaseEntity
    {
        public required string Name { get; set; }
        public char Sex { get; set; }
        public DateTime? DoB { get; set; }
        public string? Notes { get; set; }

        #region Foreign Keys
        public string UserId { get; set; } = null!;
        public Guid SpeciesId { get; set; }
        public Guid MorphId { get; set; }
        public Guid FeedId { get; set; }
        public Guid ShedId { get; set; }
        public Guid WeightId { get; set; }
        public Guid DefecationId { get; set; }
        #endregion

        #region Navigation Properties
        public virtual User User { get; set; } = null!;
        public virtual Species Species { get; set; } = null!;
        public virtual Morph Morph { get; set; } = null!;
        public virtual Feed Feed { get; set; } = null!;
        public virtual Shed Shed { get; set; } = null!;
        public virtual Weight Weight { get; set; } = null!;
        public virtual Defecation Defecation { get; set; } = null!;
        #endregion
    }
}
