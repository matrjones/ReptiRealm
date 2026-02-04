namespace ReptiRealm_WebApp.DTOs
{
    public class AddReptileDto
    {
        public string Name { get; set; } = null!;
        public char? Sex { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Guid? SpeciesId { get; set; }
        public Guid[]? MorphIds { get; set; }
    };
}
