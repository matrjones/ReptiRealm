namespace ReptiRealm_WebApp.Models
{
    public class Defecation
    {
        public Guid ReptileId { get; set; }
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; } = null!;
        public string? Notes { get; set; }
    }
}
