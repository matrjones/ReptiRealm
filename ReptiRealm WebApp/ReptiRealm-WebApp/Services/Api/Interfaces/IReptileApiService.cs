using ReptiRealm_WebApp.Models.DTOs;

namespace ReptiRealm_WebApp.Services.Api.Interfaces;

public interface IReptileApiService
{
    Task<List<ReptileCardModel>?> GetAllReptiles();
    Task<Reptile?> CreateReptile(Reptile reptile);
}
