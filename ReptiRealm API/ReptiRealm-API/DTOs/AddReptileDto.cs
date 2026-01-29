namespace ReptiRealm_API.DTOs
{
    public record AddReptileDto
    (
        string Name,
        char? Sex,
        DateTime? DateOfBirth,
        Guid? SpeciesId,
        Guid[]? MorphIds
    );
}
