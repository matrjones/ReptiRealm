using ReptiRealm_API.Domain.Enums;

namespace ReptiRealm_API.Domain.DTOs
{
    public record AddDefecationDto
    (
        DateTime? Date,
        DefecationType? Type,
        string? Notes
    );
}
