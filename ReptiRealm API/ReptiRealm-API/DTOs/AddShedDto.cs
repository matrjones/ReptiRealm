using ReptiRealm_API.Enums;

namespace ReptiRealm_API.DTOs
{
    public record AddShedDto
    (
        DateTime? Date,
        ShedRating? Rating,
        string? Notes
    );
}
