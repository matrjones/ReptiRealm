namespace ReptiRealm_WebApp.Models
{
    public class Reptile
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public char? Sex { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Guid? SpeciesId { get; set; }
        public Guid? MorphIds { get; set; }
        public string? Notes { get; set; }
    }
}
