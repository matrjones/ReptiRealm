namespace ReptiRealm_WebApp.Models
{
    public class FoodType
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Notes { get; set; }
    }
}
