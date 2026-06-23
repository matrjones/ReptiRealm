using ReptiRealm_WebApp.Models.DTOs;
using ReptiRealm_WebApp.Services.Api.Interfaces;
using ReptiRealm_WebApp.Services.Auth.Interfaces;

public class ReptileApiService : ApiService, IReptileApiService
{
    public ReptileApiService(HttpClient http, ITokenService tokenService) : base(http, tokenService)
    {
    }

    public async Task<List<ReptileCardModel>?> GetAllReptiles()
    {
        var result = await GetAsync<List<Reptile>>("reptile");

        return result?.Select(s => new ReptileCardModel
        {
            Id = s.Id!.Value,
            Name = s.Name,
            Sex = s.Sex,
            Species = s.Species?.Name
        }).ToList();
    }

    public async Task<Reptile?> CreateReptile(Reptile reptile)
    {
        var result = await PostAsync<Reptile>("reptile/create", reptile);

        return result;
    }
}