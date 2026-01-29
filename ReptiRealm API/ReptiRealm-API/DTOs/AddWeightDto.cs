namespace ReptiRealm_API.DTOs
{
    public record AddWeightDto
    (
        DateTime? Date,
        decimal Value,
        string? Unit,
        string? Notes
    );
}
