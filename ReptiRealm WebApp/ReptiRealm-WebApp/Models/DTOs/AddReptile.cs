    using ReptiRealm_WebApp.Models.Enums;

    namespace ReptiRealm_WebApp.Models.DTOs
    {
        public record AddReptile
        (
            string Name,
            Sex? Sex,
            Guid? SpeciesId,
            DateTime? DateOfBirth,
            DateTime? DateObtained,
            Guid[]? MorphIds
        );
    }
