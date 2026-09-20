    using ReptiRealm_WebApp.Models.Enums;

    namespace ReptiRealm_WebApp.Models.DTOs
    {
        public record AddReptile
        (
            string Name,
            Sex? Sex,
            string? Species,
            DateTime? DateOfBirth,
            DateTime? DateObtained,
            Guid[]? MorphIds
        );
    }
