using ReptiRealm_WebApp.Enums;

namespace ReptiRealm_WebApp.Models
{
    public class Defecation
    {
        public Guid ReptileId { get; set; }
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public DefecationType Type { get; set; }
        public string? Notes { get; set; }
    }
}
