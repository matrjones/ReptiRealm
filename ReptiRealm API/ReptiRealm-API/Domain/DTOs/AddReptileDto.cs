using ReptiRealm_API.Domain.Enums;

namespace ReptiRealm_API.Domain.DTOs
{
    public record AddReptileDto
    (
        Guid? Id,
        string Name,
        Sex? Sex,
        string? Species,
        DateTime? DateOfBirth,
        DateTime? DateObtained,
        string[]? Morphs
    );
}
