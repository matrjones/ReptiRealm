using ReptiRealm_WebApp.Models.DTOs;
using ReptiRealm_WebApp.Services.Api.Interfaces;
using ReptiRealm_WebApp.Services.Auth.Interfaces;

namespace ReptiRealm_WebApp.Services.Api
{
    public class FeedApiService : ApiService, IFeedApiService
    {
        public FeedApiService(HttpClient http, ITokenService tokenService) : base(http, tokenService)
        {
        }

        public async Task<List<string>?> GetAllAnimalTypes()
        {
            var result = await GetAsync<List<string>>("FoodType");

            return result;
        }

        public async Task<List<FoodType>?> GetAllAnimalTypeSizes(string animalType)
        {
            var result = await GetAsync<List<FoodType>>($"FoodType/Size/{animalType}");

            return result;
        }
    }
}
