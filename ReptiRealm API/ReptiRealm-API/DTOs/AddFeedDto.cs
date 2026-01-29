namespace ReptiRealm_API.DTOs
{
    public record AddFeedDto
    (
        DateTime? Date,
        int? Amount,
        bool? IsEaten,
        Guid FoodTypeId,
        string? Notes
    );
}
