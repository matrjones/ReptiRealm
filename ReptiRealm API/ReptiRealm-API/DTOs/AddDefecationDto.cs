using ReptiRealm_API.Enums;

namespace ReptiRealm_API.DTOs
{
    public record AddDefecationDto
    (
        DateTime? Date,
        DefecationType? Type,
        string? Notes
    );
}
