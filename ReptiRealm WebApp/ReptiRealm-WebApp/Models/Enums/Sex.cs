namespace ReptiRealm_WebApp.Models.Enums
{
    public enum Sex
    {
        Male,
        Female,
        Unknown
    }

    public static class SexExtensions
    {
        public static string GetSymbol(this Sex s) => s switch
        {
            Sex.Male => "bi bi-gender-male",
            Sex.Female => "bi bi-gender-female",
            _ => string.Empty
        };
    }
}
