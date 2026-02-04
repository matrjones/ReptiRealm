namespace ReptiRealm_WebApp.DTOs
{
    public class AddWeightDto
    {
        public DateTime? Date { get; set; }
        public decimal Value { get; set; }
        public string? Unit { get; set; }
        public string? Notes { get; set; }
    }
}
