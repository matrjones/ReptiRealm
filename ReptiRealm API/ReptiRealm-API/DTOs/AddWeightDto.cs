using ReptiRealm_API.Enums;

namespace ReptiRealm_API.DTOs
{
    public record AddWeightDto
    (
        DateTime? Date,
        decimal Value,
        WeightUnit? Unit,
        string? Notes
    );
}
