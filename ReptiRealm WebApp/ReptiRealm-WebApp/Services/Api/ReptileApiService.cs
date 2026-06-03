using ReptiRealm_WebApp.Models.DTOs;
using ReptiRealm_WebApp.Services.Api;
using ReptiRealm_WebApp.Services.Api.Interfaces;

public class ReptileApiService : ApiService, IReptileApiService
{
    public ReptileApiService(HttpClient http) : base(http)
    {
    }

    public async Task<List<ReptileCardModel>?> GetAllReptiles()
    {
        var result = await GetAsync<List<Reptile>>("reptile");

        return result?.Select(s => new ReptileCardModel
        {
            Id = s.Id,
            Name = s.Name,
            Sex = s.Sex,
            Species = s.Species?.Name
        }).ToList();
    }
}