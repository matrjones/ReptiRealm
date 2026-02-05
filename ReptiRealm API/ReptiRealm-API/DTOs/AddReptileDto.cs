using ReptiRealm_API.Enums;

namespace ReptiRealm_API.DTOs
{
    public record AddReptileDto
    (
        string Name,
        Sex? Sex,
        DateTime? DateOfBirth,
        Guid? SpeciesId,
        Guid[]? MorphIds
    );
}
