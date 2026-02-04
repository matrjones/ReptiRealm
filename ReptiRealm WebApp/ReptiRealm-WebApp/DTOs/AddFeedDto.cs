namespace ReptiRealm_WebApp.DTOs
{
    public class AddFeedDto
    {
        public DateTime? Date { get; set; }
        public int? Amount { get; set; }
        public bool? IsEaten { get; set; }
        public Guid FoodTypeId { get; set; }
        public string? Notes { get; set; }
    }
}
