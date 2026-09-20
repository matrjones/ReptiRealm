using ReptiRealm_API.Domain.Enums;

namespace ReptiRealm_API.Domain.DTOs
{
    public record AddReptileDto
    (
        string Name,
        Sex? Sex,
        string? Species,
        DateTime? DateOfBirth,
        DateTime? DateObtained,
        Guid[]? MorphIds
    );
}
