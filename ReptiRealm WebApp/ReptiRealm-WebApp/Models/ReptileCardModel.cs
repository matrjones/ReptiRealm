using ReptiRealm_WebApp.Models.Enums;

public class ReptileCardModel
{
    public string Name { get; set; } = string.Empty;
    public string? Species {  get; set; }
    public Sex Sex { get; set; } = Sex.Unknown;
}
