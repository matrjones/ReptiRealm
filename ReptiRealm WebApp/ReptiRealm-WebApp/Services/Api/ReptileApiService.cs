using ReptiRealm_WebApp.Models.DTOs;
using ReptiRealm_WebApp.Pages.Home.Models;
using ReptiRealm_WebApp.Services.Api.Interfaces;
using ReptiRealm_WebApp.Services.Auth.Interfaces;

namespace ReptiRealm_WebApp.Services.Api
{
    public class ReptileApiService : ApiService, IReptileApiService
    {
        public ReptileApiService(HttpClient http, ITokenService tokenService) : base(http, tokenService)
        {
        }

        public async Task<List<ReptileCardDto>?> GetAllReptiles()
        {
            var result = await GetAsync<List<Reptile>>("reptile");

            return result?.Select(s => new ReptileCardDto
            {
                Id = s.Id,
                Name = s.Name,
                Sex = s.Sex,
                Species = s.Species?.Name,
                DateOfBirth = s.DateOfBirth,
                DateObtained = s.DateObtained
            }).ToList();
        }

        public async Task<Reptile?> AddReptile(AddReptile reptile)
        {
            return await PostAsync<Reptile>("reptile/create", reptile);
        }
    }
}