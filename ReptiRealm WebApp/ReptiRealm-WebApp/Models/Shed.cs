namespace ReptiRealm_WebApp.Models
{
    public class Shed
    {
        public Guid ReptileId { get; set; }
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public char Rating { get; set; }
        public string? Notes { get; set; }
    }
}
