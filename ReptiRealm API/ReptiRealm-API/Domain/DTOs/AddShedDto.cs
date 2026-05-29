using ReptiRealm_API.Domain.Enums;

namespace ReptiRealm_API.Domain.DTOs
{
    public record AddShedDto
    (
        DateTime? Date,
        ShedRating? Rating,
        string? Notes
    );
}
