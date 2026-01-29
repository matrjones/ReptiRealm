namespace ReptiRealm_API.DTOs
{
    public record AddDefecationDto
    (
        DateTime? Date,
        string? Type,
        string? Notes
    );
}
