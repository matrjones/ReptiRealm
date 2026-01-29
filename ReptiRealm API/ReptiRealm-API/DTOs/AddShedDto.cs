namespace ReptiRealm_API.DTOs
{
    public record AddShedDto
    (
        DateTime? Date,
        char? Rating,
        string? Notes
    );
}
