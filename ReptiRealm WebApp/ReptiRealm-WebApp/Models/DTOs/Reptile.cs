using ReptiRealm_WebApp.Models.Enums;

namespace ReptiRealm_WebApp.Models.DTOs
{
    public class Reptile
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
        public Sex Sex { get; set; } = Sex.Unknown;
        public DateTime? DateOfBirth { get; set; }
        public string? Notes { get; set; }
        public string UserId { get; set; } = null!;
        public Species? Species { get; set; }
    }
}
