using ReptiRealm_API.Domain.Enums;

namespace ReptiRealm_API.Domain.DTOs
{
    public record AddReptileDto
    (
        string Name,
        Sex? Sex,
        Guid? SpeciesId,
        DateTime? DateOfBirth,
        DateTime? DateObtained,
        Guid[]? MorphIds
    );
}
