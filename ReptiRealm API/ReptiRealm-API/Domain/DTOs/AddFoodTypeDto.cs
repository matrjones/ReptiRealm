namespace ReptiRealm_API.Domain.DTOs
{
    public record AddFoodTypeDto
    (
        string AnimalType,
        string Size,
        string? Notes
    );
}
