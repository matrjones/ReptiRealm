    using ReptiRealm_WebApp.Models.Enums;

    namespace ReptiRealm_WebApp.Models.DTOs
    {
        public class AddReptile
        {
        
            public string Name {  get; set; }
            public Sex? Sex { get; set; }
            public string? Species { get; set; }
            public DateTime? DateOfBirth { get; set; }
            public DateTime? DateObtained { get; set; }
            public string[]? MorphIds { get; set; }
        }
    }
