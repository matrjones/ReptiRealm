using ReptiRealm_API.Domain.Enums;

namespace ReptiRealm_API.Domain.DTOs
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
