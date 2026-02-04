namespace ReptiRealm_WebApp.Models
{
    public class Feed
    {
        public Guid ReptileId { get; set; }
        public Guid FoodTypeId { get; set; }
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public bool IsEaten { get; set; }
        public string? Notes { get; set; }
    }
}
