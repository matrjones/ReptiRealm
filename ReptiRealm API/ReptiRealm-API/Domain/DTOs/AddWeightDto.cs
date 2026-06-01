using ReptiRealm_API.Domain.Enums;

namespace ReptiRealm_API.Domain.DTOs
{
    public record AddWeightDto
    (
        DateTime? Date,
        decimal Value,
        WeightUnit? Unit,
        string? Notes
    );
}
