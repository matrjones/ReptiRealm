namespace ReptiRealm_WebApp.Models.DTOs;

public class FoodType
{
    public Guid Id { get; set; }
    public required string AnimalType { get; set; }
    public required string Size { get; set; }
}
